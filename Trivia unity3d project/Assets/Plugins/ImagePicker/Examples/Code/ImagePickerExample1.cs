#if UNITY_ANDROID

/*
 * Image Picker for Android
 * File: ImagePickerExample1.cs
 * Description: Options example to control the different inputs for the plugin
 * 
 * You can use this code in your applications, or write more optimized code for your specific needs.
 * 
 * Created by: Elicit Ice
 * 
*/

using UnityEngine;

namespace ElicitIce {
    public class ImagePickerExample1 : ImagePicker {
        public Texture2D image;

        GUIContent[] sizeGUI = new GUIContent[]{
            new GUIContent("256"),
            new GUIContent("512"),
            new GUIContent("1024"),
            new GUIContent("2048"),
            new GUIContent("4096"),
            new GUIContent("4100"),
        };

        int[] sizes = new int[]{
            256,512,1024,2048,4096, 4100
        };

        int sizeX = 0;
        int sizeY = 0;

        public bool useCamera  = false;
        public bool useDefault = false;
        public bool bestFit    = true;

        public new GUITexture guiTexture;

        internal void Start() {
            data = new ImagePickerData();
            {
                data.loadImage = image;
                data.fileName = null;
                data.gameObject = gameObject.name;
                data.callback = ImagePickerCallback;
            }
        }

        ImagePickerData data = null;

        public GUIStyle boxStyle = new GUIStyle();

        void OnGUI() {
            GUILayoutOption[] opts = new GUILayoutOption[] { GUILayout.Height(Screen.height / 8), GUILayout.Width(Screen.width / 3) };

            if(image != null) {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), image, boxStyle);
            }
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Height(Screen.height));
            {
                useCamera = GUILayoutToggleButton("Allow Camera: ", useCamera, opts);
                bestFit = GUILayoutToggleButton("Best fit: ", bestFit, opts);
                useDefault = GUILayoutToggleButton("Use Default: ", useDefault, opts);

                GUILayout.BeginVertical(GUI.skin.box, opts[1]);
                {
                    GUILayout.Label("X max: ");
                    sizeX = GUILayout.SelectionGrid(sizeX, sizeGUI, 6, opts);
                    GUILayout.Label("Y max: ");
                    sizeY = GUILayout.SelectionGrid(sizeY, sizeGUI, 6, opts);
                }
                GUILayout.EndVertical();

                GUILayout.FlexibleSpace();

                if(GUILayout.Button("Get Image " + sizes[sizeX] + " x " + sizes[sizeY], opts)) {
                    data.bestFit = bestFit;
                    data.showCamera = useCamera;
                    data.useDefault = useDefault;
                    data.fileSubDir = "xxd";
                    data.maxWidth = sizes[sizeX];
                    data.maxHeight = sizes[sizeY];

                    StartImagePicker(data);
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            if(image!=null)
                GUILayout.Box("Loaded: " + image.width + " " + image.height);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private bool GUILayoutToggleButton(string prefix, bool me, params GUILayoutOption[] opts) {
            if(GUILayout.Button(prefix + (me ? "On" : "Off"), opts)) {
                me = !me;
            }
            return me;
        }

        public override void ImagePickerCallback(string result) {
            base.ImagePickerCallback(result);
            image = data.loadImage;
        }
    }
}

#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif