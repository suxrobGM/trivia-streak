#if UNITY_ANDROID
/*
 * Image Picker for Android
 * File: ImagePickerExample2.cs
 * Description: Image Loader that loads previously assigned images, based on the GameObject Name
 * 
 * You can use this code in your applications, or write more optimized code for your specific needs.
 * 
 * Created by: Elicit Ice
 * 
*/

using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace ElicitIce {
    /// <summary>
    /// Requires a unique gameobject name
    /// </summary>
    public class ImagePickerExample2 : MonoBehaviour {

        //example for recycled use, using a bool to keep track if we are already busy
        public static bool busy = false;

        public static bool showCam = true;
        public static bool useDefault = false;

        private ImagePicker imagePick = null;
        private ImagePickerData data = null;

        // Local variables
        public Material material;
        public MeshRenderer render;

        public static String subDir = "PhotoCube"; // optional set to null to diable

        //alternative way to get the path to files
        private string path {
            get { 
                if(string.IsNullOrEmpty(subDir))
                    return imagePick.GetInternalDir() + "/" + gameObject.name + ".png";
                else
                    return imagePick.GetInternalDir() + "/" + subDir + "/" + gameObject.name + ".png";
            }
        }

        internal void OnSettingChange() {
            if(data != null) {
                data.showCamera = showCam;
                data.useDefault = useDefault;
            }
        }

        internal void Start() {
            Setup();
        }

        void Setup() {
            if( imagePick == null )
                imagePick = SmartMonoBehaviour.FindOrCreate<ImagePicker>();

            //if(data == null)
            {
                data = new ImagePickerData();
                data.fileSubDir = subDir;
                data.maxHeight = 512;
                data.maxWidth = 512;
                data.quality = -1; //force png's so we can get transparant items
                data.bestFit = true;

                data.showCamera = showCam;
                data.useDefault = useDefault;

                data.gameObject = imagePick.name;
                data.callback = imagePick.ImagePickerCallback;
                data.callbackError = OnError;
                
                //data.fileType = "image/png"; //we could change it to Mime Types like */* or image/png, make sure you test any changes in this, as it might lead to segmentation faults if misused.
                //only mimetypes that start with "image" will be copied and resized, other files will be returned as a path to their original location
            }

            StartCoroutine(ScanForFile());
        }

        private void OnError() {
            busy = false;
        }

        private void SetTextureOnMaterial(Texture2D texture) {
            GetComponent<Renderer>().material = material;
            material.mainTexture = texture;
            material.color = Color.white;
            busy = false;

            Debug.Log(name + " loaded " + texture.name);
        }

        void OnMouseUpAsButton() {
            if(busy)
                return;
            busy = true;

            //Declare the differences between the instances of this class
            data.fileName = gameObject.name;
            data.DialogTitle = "Replace Photo Cube " + gameObject.name;
            data.imageSetter = SetTextureOnMaterial;
            //Run it, knowing the others of this class will wait for the imageSetter to invoke and clear the busy flag
            imagePick.StartImagePicker(data);
        }

        /// <summary>
        /// at start wait for others to finish, 
        /// then find the previously loaded file for this object
        /// If it exists, load it
        /// 
        /// coroutines are not really threadded 
        /// so as long as we know only one is processing at a time, 
        /// we can check busy and set it in the same coroutine 
        /// as long as we do not yield in between
        /// 
        /// </summary>
        IEnumerator ScanForFile() {
            while(busy) {
                yield return new WaitForFixedUpdate();
            }
            String path = this.path;
            if(File.Exists(path)) {
                busy = true;
                data.imageSetter = SetTextureOnMaterial;

                Debug.Log( "Reading:" + path );
                imagePick.ProcessImage( data, path );
            }
        }
        
        /// <summary>
        /// Remove the created image from the SD Card
        /// </summary>
        void DeleteFile() {
            if(File.Exists(path)) {
                File.Delete(path);
            }
        }
    }
}
#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif