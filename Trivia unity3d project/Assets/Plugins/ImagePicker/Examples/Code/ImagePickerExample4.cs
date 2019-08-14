#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElicitIce {
    public class ImagePickerExample4 : SmartMonoBehaviour {

#if !UNITY_EDITOR  || DEBUG
        ImagePicker imagePicker;
        ImagePickerData data;
#endif

        public List<Example4Flyer> galleryFlyers;
        public Vector3 rotationOffset;
        public Transform lookAt;
        public Material material;
        
        public int   imgsPerRow         = 12;
        public float imgsDistance       = 10;
        public float imgsYoffsetPerRow  = 3;

        public Timer update  = new Timer(5, true);

        //give the plugin a maximum of 5 seconds before loading the next one
        //resets sooner if we receive the image
        public Timer timeOut = new Timer(5, true);

        int items = 0;

        public bool started = false;

        public override void Start() {
            base.Start();
            if(items > 0)
                items = 0;
#if !UNITY_EDITOR || DEBUG
            imagePicker = GetOrCreateComponent<ImagePicker>(this);

            data = new ImagePickerData(null, null, "Gallery", 512, 512, true, true);
            data.gameObject = gameObject.name;
            data.callback = CustomeReceiver;
#endif
            lookAt = Camera.main.transform;
        }

        void OnGUI() {
            GUILayoutOption height = GUILayout.Height(Screen.height * 0.05f);

#if UNITY_EDITOR && !DEBUG
        if(GUILayout.Button("Add dummy flyer", height))
        {
            AddFlyer();
            UpdateFlyers();
        }
        if(GUILayout.Button("Update", height))
            UpdateFlyers();
#else
            GUILayout.BeginHorizontal();
            {
                if(GUILayout.Button("Open Full Picker", height)) {
                    SetData( false, true, false );
                    imagePicker.StartImagePicker(data);
                }
                if(GUILayout.Button("Open Default Camera", height)) {
                    SetData( true , true, false );
                    imagePicker.StartImagePicker(data);
                }
                if(GUILayout.Button("Open Default Picker", height)) {
                    SetData( true, false, false );
                    imagePicker.StartImagePicker(data);
                }
                if( GUILayout.Button( "Open Multiple Select", height ) ) {
                    SetData( false, false, true );
                    imagePicker.StartImagePicker( data );
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("Received images waiting for processing: " + items);
            if(!started && GUILayout.Button("Begin receiving")) {
                started = true;
            }

            if(!timeOut.IsDone)
                GUILayout.Label("Processing, please wait " + timeOut.ToString(2, "s"));
#endif
        }

#if !UNITY_EDITOR || DEBUG
        private void SetData( bool useDefault, bool useCamera, bool multiple ) {
            data.useDefault = false;
            data.showCamera = false;
            data.selectMultiple = true;        
        }

        void Update() {

            if(update) {
                update.Start();
                items = imagePicker.GetReceivedCount();
            }

            if(started) {
                if(items > 0) {
                    if(timeOut) {
                        imagePicker.ReceiveFile(data, 0);
                        timeOut.Start();
                    }
                } else {
                    started = false;
                }
            }
        }
#endif

        private Example4Flyer AddFlyer() {
            GameObject gobj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Example4Flyer fly = gobj.AddComponent<Example4Flyer>();
            gobj.SetActive(false);
            fly.transform.parent = _transform;
            fly.GetComponent<Renderer>().sharedMaterial = material;

            galleryFlyers.Add(fly);
            return fly;
        }

        public int rows = 0;

        private void UpdateFlyers() {
            int itemCount = galleryFlyers.Count;
            rows = Mathf.CeilToInt(itemCount / imgsPerRow);

            float yStart = (imgsYoffsetPerRow * (rows - 1)) * -0.5f;

            float yOffset = 0;
            int curItem = imgsPerRow + 1;
            int curRow = -1;
            float angleIncr = 1;
            float angle = 0;

            foreach(Example4Flyer img in galleryFlyers) {
                curItem++;
                if(curItem < imgsPerRow) {
                    angle += angleIncr;
                } else {
                    curRow++;
                    curItem = 0;
                    angle = 0;
                    //calc different angle based on number of pics for this row?
                    angleIncr = 360 / Mathf.Min(imgsPerRow, itemCount);
                    yOffset = yStart + imgsYoffsetPerRow * curRow;
                }

                img.SetPositionAround(lookAt, _transform.position, imgsDistance, yOffset, angle, rotationOffset);

                img.name = curRow + " " + curItem;

                itemCount--;
                img.gameObject.SetActive(true);
            }
        }

#if !UNITY_EDITOR || DEBUG
        /// <summary>
        /// Example callback from plugin, matching both possible callbacks
        /// </summary>
        /// <param name="value">the filename (or names) of the selected file</param>
        internal void CustomeReceiver(string result) {
            int index = -1;
            string[] parts = result.Split('|');

            string load = null;

            if(parts.Length == 1) {
                //picker result
                load = parts[0];
            } else {
                if(parts.Length != 2 || !int.TryParse(parts[0], out index)) {
                    Debug.LogError("Selected callback was not valid for the executed action");
                    return;
                }

                load = parts[1];

                //manually remove the entry
                imagePicker.RemoveReceivedEntry(index);
            }


            if(string.IsNullOrEmpty(load)) {
                //use logcat to get more information
                Debug.Log("SDCard full or invalid file selected, please try again");
                return;
            }

            Example4Flyer fly = AddFlyer();

            StartCoroutine(LoadImage(load, texture => fly.image = texture));
        }

        /// <summary>
        /// Example image loader
        /// </summary>
        /// <param name="file">the filepath to attempt to load</param>
        /// <param name="imageSetter">A lambda operation or other Action that takes a Texture2D
        /// Example: 
        /// texture => yourTexture = texture
        /// </param>
        /// <returns></returns>
        IEnumerator LoadImage(string file, System.Action<Texture2D> imageSetter) {
            WWW www = new WWW(@"file://" + file);
            yield return www;
            if(string.IsNullOrEmpty(www.error)) {
                Texture2D tex = new Texture2D(1, 1);
                www.LoadImageIntoTexture(tex);
                imageSetter.Invoke(tex);
                timeOut.Stop();

                UpdateFlyers();
            } else {
                Debug.LogError(www.error);
            }
        }
#endif

    }
}

#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif