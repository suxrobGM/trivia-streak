using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Basic gui for Example 5
/// Please check ImagePickerExample5.cs for more details
/// </summary>
public class Example5gui : MonoBehaviour {
    Texture2D showMe;
    List<string> errors = new List<string>();
    Vector2 scrollPosition;
    public int maxErrors = 100;

    string lastFilename = "NoBlues";
    string lastPath = null;

    GUIStyle textStyle;

    // Use this for initialization
    void Start() {
        InitImagePlugins();
    }

    // Update is called once per frame
    void OnGUI() {
        if(textStyle == null) {
            textStyle = new GUIStyle(GUI.skin.textArea);
            textStyle.richText = true;
        }

        GUILayoutOption[] btnOpts= new GUILayoutOption[]{
            GUILayout.Height(Screen.height * 0.12f)
        };

        if(showMe != null) {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), showMe);
        }

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical();
            {
                if(GUILayout.Button("Get Image from any App", btnOpts)) GetImage();
                if(GUILayout.Button("Get Camera Image", btnOpts)) GetCameraImage();
                if(GUILayout.Button("Get Album Image", btnOpts)) GetGaleryImage();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            {
                if(showMe != null && GUILayout.Button("Remove all blue and save to gallery", btnOpts)) RemoveBlueAndSave();
                if(!string.IsNullOrEmpty(lastPath) && GUILayout.Button("Load: " + lastPath, btnOpts)) {
					#if UNITY_ANDROID
                    LoadImage(lastPath);
					#endif
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

        GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.07f;
        GUILayout.BeginArea(new Rect(0, Screen.height * 0.7f, Screen.width, Screen.height * 0.29f));
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            foreach(string msg in errors) {
                GUILayout.TextArea(msg, textStyle);
            }
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Simple example of how to manipulate pixels and storing them    
    /// </summary>
    private void RemoveBlueAndSave() {
        Color[] clrs = showMe.GetPixels();
        for(int i = 0; i < clrs.Length; i++)
            clrs[i].b = 0;
        showMe.SetPixels(clrs);
        showMe.Apply();
        byte[] bytes = showMe.EncodeToPNG();

        SaveImage(bytes, lastFilename, lastFilename, "All blue removed");
    }

    void Logging(string msg) {
        if(errors.Count >= maxErrors)
            errors.RemoveRange(0, errors.Count - maxErrors);

        errors.Add(msg);
    }

    /// <summary>
    /// We created a method for different platforms, which might or might not match the simple callback signature of ImagePicker for Android
    /// </summary>
    /// <param name="path">the absolute file path</param>
    /// <param name="notMatchingMySignatureWorkAroundImplementation">just a parameter to prevent the delegate from matching</param>
    void ReceiveImage(string path, int delegateBreaker) {
        if(string.IsNullOrEmpty(path))
            Logging("Came up empty");
        else if(delegateBreaker == 1) {
            lastPath = path;
            OnDebug(lastPath);
        } else
            StartCoroutine(loadImage(path));
    }

    IEnumerator loadImage(string file) {
        WWW www;
        if(file.StartsWith("http:", System.StringComparison.InvariantCultureIgnoreCase) || file.StartsWith("https:", System.StringComparison.InvariantCultureIgnoreCase))
            www = new WWW(file);
        else
            www = new WWW(@"file://" + file);

        yield return www;
        if(string.IsNullOrEmpty(www.error)) {
            if(showMe == null)
                showMe = new Texture2D(1, 1);

            lastFilename = Path.GetFileNameWithoutExtension(file);
            www.LoadImageIntoTexture(showMe);
        } else {
            Logging(www.error);
        }
    }

    /// <summary>
    /// Instance pattern helper function, finds or creates an instance within the scene
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T FindOrCreate<T>() where T : Component {
        T result = (T)FindObjectOfType(typeof(T));
        if(result == null) {
            GameObject go = new GameObject();
            result = go.AddComponent<T>();
            result.name = typeof(T).ToString();
        }
        return result;
    }

    void OnError(string msg) {
        Logging("<color=red>" + msg + "</color>");
    }
    void OnWarning(string msg) {
        Logging("<color=yellow>" + msg + "</color>");
    }
    void OnDebug(string msg) {
        Logging("<color=green>" + msg + "</color>");
    }

    void OnCancel(string msg) {
        Logging("<color=green>User Cancelled </color>");
    }

#if UNITY_ANDROID //&& !UNITY_EDITOR
    //not serialized!
    ImagePickerExample5 plugin;
    int useRawPath = 0;

    void InitImagePlugins() {
        plugin = FindOrCreate<ImagePickerExample5>();

        plugin.Initialize();
        //        plugin.onSuccessfulPhoto += ReceiveImage;
        plugin.onSuccessfulPhoto += FixParameterSignature;
        plugin.onCancel += OnCancel;    
        plugin.onError  += OnError;
        plugin.onWarning += OnWarning;
        plugin.onVerbose += OnDebug;
        plugin.onNull += Logging;
    }

    void GetImage() {
        useRawPath = 1;
        plugin.SetParameters(null, null, 1024, 512, true, true, false);
        Logging("Android platform");
        plugin.StartImagePicker();
    }

    void GetCameraImage() {
        useRawPath = 1;
        plugin.SetParameters(null, null, 1024, 512, true, true, true);
        plugin.StartImagePicker();
    }

    void GetGaleryImage() {
        useRawPath = 1;
        plugin.SetParameters(null, null, 1024, 512, true, false, true);
        plugin.StartImagePicker();
    }

    void LoadImage(string path) {
        useRawPath = 0;
        plugin.ManualLoad(path);
        //StartCoroutine(loadImage(path));
    }

    string SaveImage(byte[] bytes, string filename, string title, string description) {
        string path = plugin.GetPicturesDirectory() + "/Testing Album/";

        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        path += filename + ".png";

        File.WriteAllBytes(path, bytes);

        path = plugin.AddToGallery(path, title, description);
        OnDebug("gallery " + path);
        return path;
    }

    /// <summary>
    /// What if the signature ImagePicker implements does not match yours?
    /// Answer: implement this as a workaround
    /// </summary>
    /// <param name="path"></param>
    void FixParameterSignature(string path) {
        //you can create/add the desired parameters here
        ReceiveImage(path, useRawPath);
    }
#elif UNITY_IPHONE && ! UNITY_EDITOR
    
    void InitImagePlugins() { Logging("iPhone"); }

    void GetImage() {
        OnDebug("info iPhone example");
        OnWarning("warning iPhone example");
        OnError("error iPhone example");
    }

    void GetCameraImage() { GetImage(); }

    void GetGaleryImage() { GetImage(); }

    string SaveImage(byte[] bytes, string filename, string title, string description) {  return null;  }
#else
    void InitImagePlugins() { Logging("Generic platform"); }

    void GetImage() {
        OnDebug("info generic platform example");
        OnWarning("warning generic platform example");
        OnError("error generic platform example");
    }

    void GetCameraImage() { GetImage(); }

    void GetGaleryImage() { GetImage(); }

    void LoadImage(string path) { GetImage(); }

    string SaveImage(byte[] bytes, string filename, string title, string description) { return null; }

#endif
}
