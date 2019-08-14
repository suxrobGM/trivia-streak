using UnityEngine;
using System;


#if UNITY_ANDROID //&& !UNITY_EDITOR
using ElicitIce;

/// <summary>
/// 
/// Example 5, codenamed ImagePickerLite, an event driven, ready for implementation in a cross platform project
/// This code does not inherit from the ImagePicker.cs, it copied some functions but is a much smaller/lighter version
/// This example also does not use the ElictIce namespace
/// Still relies on ImagePickerData.cs, it contains a region of code that can be removed if desired.
///
/// For a quick test, use Example5gui.cs, it will create an object with this script on runtime if it is running on android. 
///
/// add: 
/// <activity android:name="com.ElicitIce.Plugin.ImagePicker" android:label="@string/app_name" ></activity>
/// and 
/// <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" /> 
/// 
/// to the manifest
///
/// TODO: Add received files code
/// 
/// </summary>
public class ImagePickerExample5 : MonoBehaviour {
    public delegate void ReceiveString(string pathToFile);

    public event ReceiveString onSuccessfulPhoto;
    public event ReceiveString onCancel;
    public event ReceiveString onNull;
    public event ReceiveString onError;
    public event ReceiveString onWarning;
    public event ReceiveString onVerbose;

    private AndroidJavaClass imagePicker = null;
    private ImagePickerData data;
    private bool initialized = false;

    public int debug = 3;

    /// <summary>
    /// Check if we are ready
    /// </summary>
    /// <returns>true if not ready</returns>
    public bool Initialize() {
        if(initialized)
            return false;

        if(AndroidJNI.AttachCurrentThread() != 0) {
            Debug.LogError("No Java VM to JNI with");
            return true;
        }

        try {
            imagePicker = new AndroidJavaClass("com.ElicitIce.Plugin.ImagePicker");
        } catch(Exception e) {
            OnError("Error loading plugin: " + e.ToString());
        }

        if(imagePicker == null) {
#if UNITY_EDITOR
            string error = "JNI does not work inside the editor";
#else
            string error = "JNI seems to be non-functional, check the manifest and the editor log if it included the ImagePicker.jar file.";
#endif
            Debug.LogError(error);
            OnError(error);

            return true;
        }

        //set debug to receive more logcat data
        imagePicker.SetStatic("debug", debug);

        initialized = true;
        return false;
    }

    /// <summary>
    /// Start the Image Picker Activity
    /// </summary>
    /// <param name="data">a populated ImagePickerData</param>
    public void StartImagePicker() {
        if(Initialize())
            return;

        //it needs a texture before it can load
        if(data.imageSetter == null && data.loadImage == null)
            data.loadImage = new Texture2D(1, 1);

        imagePicker.CallStatic("selectImage", data.setting);
    }

    /// <summary>
    /// Cache a raw path using Image Picker Activity
    /// </summary>
    /// <param name="data">a populated ImagePickerData</param>
    public void ManualLoad(string rawpath) {
        if(Initialize())
            return;

        //it needs a texture before it can load
        if(data.imageSetter == null && data.loadImage == null)
            data.loadImage = new Texture2D(1, 1);

        imagePicker.CallStatic("openFile", data.setting, rawpath);
    }

    /// <summary>
    /// Example callback from plugin, matching the ImageResultFunc delegate
    /// </summary>
    /// <param name="value">the filename (or names) of the selected file</param>
    void LiteCallback(string result) {
        if(string.IsNullOrEmpty(result)) {
            //Check logcat for more details
            OnNull("Invalid file selected or something went wrong, please try again.");
            return;
        }
        OnSuccess(result);
    }

    public string GetPicturesDirectory() {
        if(Initialize())
            return null;

        return imagePicker.CallStatic<string>("getPicturesDir");
    }

    public string AddToGallery(string path, string title, string description) {
        if(Initialize())
            return null;

        return imagePicker.CallStatic<string>("addImageToGallery", path, title, description);
    }

    public void SetParameters(string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera, bool useDefault) {
        if(Initialize())
            return;

        data = new ImagePickerData();
        data.bestFit = bestFit;
        data.showCamera = showCamera;
        data.useDefault = useDefault;

        data.fileName = fileName;
        data.fileSubDir = subDir;
        data.maxWidth = maxWidth;
        data.maxHeight = maxHeight;
        data.gameObject = name;
        data.callback = LiteCallback;
        data.callbackOnCancel = OnCancel;

        //override mode for testing
        data.mode = 4;

        //comment any of these out to remove them
        data.debugError = OnError;
        data.debugWarning = OnWarning;
        data.debugVerbose = OnVerbose;
    }

    void GenericEvent(ReceiveString func, string msg) {
        if(func != null)
            func.Invoke(msg);
    }

    void OnCancel(string msg) {
        GenericEvent(onCancel, msg);
    }

    void OnNull(string msg) {
        GenericEvent(onNull, msg);
    }

    void OnError(string msg) {
        GenericEvent(onError, msg);
    }

    void OnWarning(string msg) {
        GenericEvent(onWarning, msg);
    }

    void OnVerbose(string msg) {
        GenericEvent(onVerbose, msg);
    }

    void OnSuccess(string msg) {
        GenericEvent(onSuccessfulPhoto, msg);
    }
}
#elif UNITY_EDITOR //ideally place the endif here and remove the lines below
public class ImagePickerExample5 : MonoBehaviour {
    public int debug = 3;

    public delegate void ReceiveString(string pathToFile);

    public event ReceiveString onSuccessfulPhoto;
    public event ReceiveString onNull;
    public event ReceiveString onCancel;
    public event ReceiveString onError;
    public event ReceiveString onWarning;
    public event ReceiveString onVerbose;

    public bool Initialize() { return false; }
    public void StartImagePicker() {
        //to remove the warnings in the editor:
        if(onNull == null || onError == null || onWarning == null || onVerbose == null || onSuccessfulPhoto == null || onCancel == null)
            return;
    }
    public void SetParameters(string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera, bool useDefault) {}

    public string GetPicturesDirectory() { return ""; }
    public string AddToGallery(string path, string title, string description) { return null; }
}
#endif