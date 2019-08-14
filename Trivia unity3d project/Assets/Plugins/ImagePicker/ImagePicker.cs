/*
 * Image Picker for Android
 * File: ImagePicker.cs
 * Description: interface example for accessing the plugin
 * 
 * You can use this code in your applications, or write more optimized code for your specific needs.
 * The examples show quick examples, the best method to use is to manually create and populate the values of ImagePickerData and pass that to the plugin
 * This class contains helper functions which are slightly more bulky, but if regular use is to only load one or two images they are way more convenient
 * 
 * Created by: Elicit Ice
 * 
*/

/* No longer need this, though in some cases there might be a reason to be able to change this.
 * The plugin has functions for calling with an alternative activity 
 * Uncomment the define to enable manual control over selecting the currentActivity
*/
//#define DEFAULT_ACTIVITY

using System.Collections;
using UnityEngine;

namespace ElicitIce {
    //added DEBUG for editing with MS Visual Studio
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
    /// <summary>
    /// Image picker plugin interface
    /// </summary>
    public class ImagePicker : MonoBehaviour {
        /// <summary>
        /// Reference to the plugin
        /// </summary>
        private AndroidJavaClass imagePicker = null;
        private AndroidJavaClass ajcImagePicker {
            get {
                if( imagePicker == null ) {
                    Awake();
                }
                return imagePicker;
            }
            set {
                imagePicker = value;
            }
        }

#if DEFAULT_ACTIVITY
        private AndroidJavaObject currentActivity;
#endif

        /// <summary>
        /// The current data being used for processing
        /// some settings like the Texture2D image
        /// are required to receive the texture from the plugin
        /// 
        /// Do not change this before you have received a callback or unexpected behaviour might occur
        /// </summary>
        private ImagePickerData processing;

        /// <summary>
        /// set to higher then 0 before calling a function 
        /// to get more information in the LogCat
        /// 
        /// 0 = silent (Plugin internal default)
        /// 1 = errors only
        /// 2 = warnings & errors
        /// 3 = verbose (information, warnings & errors)
        /// </summary>
        public int debug = 3;
        public int debugSet {
            get {
                if( ajcImagePicker != null ) {
                    return ajcImagePicker.GetStatic<int>( "debug" );
                }
                return -1;
            }
            set {
                if( ajcImagePicker != null ) {
                    debug = value;
                    ajcImagePicker.SetStatic( "debug", value );
                }
            }
        }

        internal virtual void Awake() {
            if( AndroidJNI.AttachCurrentThread() != 0 ) {
                Debug.LogError( "No Java VM to JNI with" );
                return;
            }

#if DEFAULT_ACTIVITY
            //Change the next line if you wish to use a different currentActivity
            AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = ajc.GetStatic<AndroidJavaObject>("currentActivity");
#endif

            ajcImagePicker = new AndroidJavaClass( "com.ElicitIce.Plugin.ImagePicker" );

            if( ajcImagePicker == null ) {
                Debug.LogError( "JNI seems to be non-functional, check the manifest and the editor log if it included the ImagePicker.jar file." );
            }

            //optional, comment the next line if you are not debugging
            if( debug != debugSet )
                debugSet = debug;
            //ajcImagePicker.SetStatic("debug", debug);
        }

        /// <summary>
        /// Images you want to submit to the gallery should go here
        /// You need to add the proper permissions to the manifest to use directories on the SD Card
        /// </summary>
        /// <returns></returns>
        public string GetExternalDir() {
            return ajcImagePicker.CallStatic<string>( "getExternalDir" );
        }

        /// <summary>
        /// Cached files are stored here
        /// </summary>
        /// <returns></returns>
        public string GetInternalDir() {
            return ajcImagePicker.CallStatic<string>( "getFileDir" );
        }

        /// <summary>
        /// Start the Image Picker Activity
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="showCamera">true: add Camera apps to picker, false: no camera apps</param>
        public void StartImagePicker( Texture2D image, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) {
            ImagePickerData data = new ImagePickerData();//image, fileName, subDir, maxWidth, maxHeight, bestFit, showCamera);
            data.loadImage = image;

            data.bestFit = bestFit;
            data.showCamera = showCamera;
            data.fileName = fileName;
            data.fileSubDir = subDir;
            data.maxWidth = maxWidth;
            data.maxHeight = maxHeight;

            data.useDefault = false;

            //these two are set for conveniance
            data.gameObject = gameObject.name;
            data.callback = ImagePickerCallback;

            StartImagePicker( data );
        }

        /// <summary>
        /// Start the Image Picker Activity
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="showCamera">true: add Camera apps to picker, false: no camera apps</param>
        public void StartImagePicker( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) {
            ImagePickerData data = new ImagePickerData( null, fileName, subDir, maxWidth, maxHeight, bestFit, showCamera );

            //these two are set for conveniance
            data.gameObject = gameObject.name;
            data.callback = ImagePickerCallback;
            data.imageSetter = imageSetter;

            StartImagePicker( data );
        }

        /// <summary>
        /// Start the Image Picker Activity
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="showCamera">true: add Camera apps to picker, false: no camera apps</param>
        public void StartImagePicker( Texture2D image, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) {
            StartImagePicker( image, null, subDir, maxWidth, maxHeight, bestFit, showCamera );
        }

        /// <summary>
        /// Start the Image Picker Activity
        /// </summary>
        /// <param name="data">a populated ImagePickerData</param>
        public void StartImagePicker( ImagePickerData data ) {
            processing = data;

            //it needs a texture before it can load
            if( data.imageSetter == null && data.loadImage == null )
                data.loadImage = new Texture2D( 1, 1 );

#if DEFAULT_ACTIVITY
            imagePicker.CallStatic("selectImage", currentActivity, data.setting);
#else
            ajcImagePicker.CallStatic( "selectImage", data.setting );
#endif
        }

        /// <summary>
        /// Start the Image Picker Activity
        /// </summary>
        /// <param name="data">a populated ImagePickerData</param>
        /// <param name="callbackHandler">unique object name to call the function on</param>
        /// <param name="function">delegate compatible function</param>
        public void StartImagePicker( ImagePickerData data, GameObject callbackHandler, ImageResultFunc function ) {
            processing = data;

            data.gameObject = callbackHandler.name;
            data.callback = function;

#if DEFAULT_ACTIVITY
            imagePicker.CallStatic("selectImage", currentActivity, data.setting);
#else
            ajcImagePicker.CallStatic( "selectImage", data.setting );
#endif
        }

        /// <summary>
        /// Get a single image from a movie (must be supported on the device)
        /// The preview image is processed just as through StartImagePicker
        /// </summary>
        /// <param name="ImagePickerData"></param>
        public void GetVideoPreview( ImagePickerData data, string videoPath ) {
            processing = data;
#if DEFAULT_ACTIVITY
            imagePicker.CallStatic("getVideoPreview", currentActivity, data.setting, receivedFilePath);
#else
            ajcImagePicker.CallStatic( "getVideoPreview", data.setting, videoPath );
#endif
        }


        /// <summary>
        /// Example callback from plugin, matching the ImageResultFunc delegate
        /// </summary>
        /// <param name="value">the filename (or names) of the selected file</param>
        public virtual void ImagePickerCallback( string result ) {
            if( string.IsNullOrEmpty( result ) ) {
                //use logcat to get more information
                Debug.Log( "SDCard full or invalid file selected, please try again" );
                if( processing.callbackError != null )
                    processing.callbackError.Invoke();
                return;
            }

            if( processing.imageSetter == null )
                StartCoroutine( LoadImage( result, processing ) );
            else
                StartCoroutine( LoadImage( result, processing.imageSetter ) );
        }

        /// <summary>
        /// Example image loader
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerator LoadImage( string file, ImagePickerData data ) {
            WWW www = new WWW( @"file://" + file );
            yield return www;
            if( string.IsNullOrEmpty( www.error ) ) {
                www.LoadImageIntoTexture( data.loadImage );
            } else {
                Debug.LogError( www.error );
            }
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
        public static IEnumerator LoadImage( string file, System.Action<Texture2D> imageSetter ) {
            WWW www = new WWW( @"file://" + file );
            yield return www;
            if( string.IsNullOrEmpty( www.error ) ) {
                Texture2D tex = new Texture2D( 1, 1 );
                www.LoadImageIntoTexture( tex );
                imageSetter.Invoke( tex );
            } else {
                Debug.LogError( www.error );
            }
        }

        /// <summary>
        /// Get the count of received file paths from external apps
        /// This could change while the app is running, 
        /// it is recommended to check this periodically 
        /// when it is convenient for your program to do so
        /// </summary>
        /// <returns>the number of files waiting to be processed</returns>
        public int GetReceivedCount() {
            return ajcImagePicker.CallStatic<int>( "getReceivedCount" );
        }

        /// <summary>
        /// Get the raw filepath of the received file entry at index
        /// </summary>
        /// <param name="index">index of the requested file entry</param>
        /// <returns>null if invalid or empty, or the raw file path (unbuffered and not processed for use as a Texture2D!)</returns>
        public string GetRawEntry( int index ) {
            return ajcImagePicker.CallStatic<string>( "getReceivedPath", index );
        }

        /// <summary>
        /// Remove an entry from the array of received files
        ///
        /// Note: at this time I found no need to make this thread safe
        /// Either the user is in you app, or in sending images from another app
        /// 
        /// New files are always added to the end of the list 
        /// You could remove index 0 untill a certain amount has been removed from the stack
        /// Do not do so when starting multiple coroutines as they would change the meaning of each others index.
        /// 
        /// </summary>
        /// <param name="index">index to start removal</param>
        /// <param name="count">optional amount of entries to remove</param>
        public void RemoveReceivedEntry( int index, int count = 1 ) {
            //the plugin will handle checking if the index is valid
            ajcImagePicker.CallStatic( "removeReceivedEntry", index, count );
        }

        /// <summary>
        /// Request a specific received file from the array of received files
        /// </summary>
        /// <param name="data">settings for loading the image</param>
        /// <param name="file">the index to attempt to load, 0 based</param>
        public void ReceiveFile( ImagePickerData data, int index ) {
            processing = data;
            //the plugin will handle checking if the index is valid
            ajcImagePicker.CallStatic( "receiveFile", data.setting, index );
        }


        /// <summary>
        /// Get the count of received texts from external apps
        /// This could change while the app is running, 
        /// it is recommended to check this periodically 
        /// when it is convenient for your program to do so
        /// </summary>
        /// <returns>the number of texts in the buffer</returns>
        public int GetReceivedTextCount() {
            return ajcImagePicker.CallStatic<int>( "getReceivedTextCount" );
        }

        /// <summary>
        /// Get the text from the received buffer
        /// </summary>
        /// <param name="index">index of the requested text</param>
        /// <returns>null if invalid or empty, or the text in position 0, the second string is the subject if available or null</returns>
        public string[] GetReceivedText( int index ) {
            return ajcImagePicker.CallStatic<string[]>( "getReceivedText", index );
        }

        /// <summary>
        /// Remove an entry from the array of received files
        ///
        /// Note: at this time I found no need to make this thread safe
        /// Either the user is in you app, or in sending images from another app
        /// 
        /// New files are always added to the end of the list 
        /// You could remove index 0 untill a certain amount has been removed from the stack
        /// Do not do so when starting multiple coroutines as they would change the meaning of each others index.
        /// 
        /// </summary>
        /// <param name="index">index to start removal</param>
        /// <param name="count">optional amount of entries to remove</param>
        public void RemoveReceivedTextEntry( int index, int count = 1 ) {
            //the plugin will handle checking if the index is valid
            ajcImagePicker.CallStatic( "removeReceivedTextEntry", index, count );
        }


        /// <summary>
        /// Request the first received file from the array of received files
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        public void ReceiveFile( Texture2D image, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit ) {
            ImagePickerData data = new ImagePickerData( image, null, subDir, maxWidth, maxHeight, bestFit );

            data.callback = ImagePickerReceive;
            data.gameObject = gameObject.name;

            ReceiveFile( data, 0 );
        }

        /// <summary>
        /// Request the first received file from the array of received files
        /// </summary>
        /// <param name="imageSetter">a System.Action that receives a texture</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        public void ReceiveFile( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit ) {
            ImagePickerData data = new ImagePickerData( null, null, subDir, maxWidth, maxHeight, bestFit );

            data.callback = ImagePickerReceive;
            data.gameObject = gameObject.name;
            data.imageSetter = imageSetter;

            ReceiveFile( data, 0 );
        }

        /// <summary>
        /// Load received images according to data, if fileName is set it will be appended with auto increment
        /// 
        /// Examples: 
        /// fileName null = use original filenames
        /// 
        /// fileName "img_" = calls the callback with images stored as img, img_2, img_3, etc...
        /// according to compression setting either PNG (< 0) or JPG (0 - 100) will be added as extension
        /// 
        /// if filetype begins with "image/"
        /// the plugin will callback with an empty path for each invalid entry
        /// while debugging check the logcat for more information on each file
        /// upon receiving a empty string, you should just inform the user that an image did not load succesfully.
        /// 
        /// </summary>
        public void ReceiveAllFiles( ImagePickerData data ) {
            processing = data;
            ajcImagePicker.CallStatic( "receiveAllFiles", data.setting );
        }

        /// <summary>
        /// Receive all files from the Image Picker Activity
        /// Optinally clear the list when done processing the list
        /// Note: no additional checks are done if the list was added to after Unity receives the last expected entry
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="removeReceived">true: after all images are done, clear the received items list</param>
        public void ReceiveAllFiles( Texture2D[] images, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool removeProcessed ) {
            ImagePickerData data = new ImagePickerData( null, fileName, subDir, maxWidth, maxHeight, bestFit );

            data.receiveImages = images;
            data.removeReceived = removeProcessed;

            //these two are set for conveniance
            data.gameObject = gameObject.name;
            data.callback = ImagePickerReceive;

            ReceiveAllFiles( data );
        }

        /// <summary>
        /// Receive all files from the Image Picker Activity
        /// Optinally clear the list when done processing the list
        /// Note: no additional checks are done if the list was added to after Unity receives the last expected entry
        /// </summary>
        /// <param name="imageSetter">system action for setting multiple textures</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="removeReceived">true: after all images are done, clear the received items list</param>
        public void ReceiveAllFiles( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool removeProcessed ) {
            ImagePickerData data = new ImagePickerData( null, fileName, subDir, maxWidth, maxHeight, bestFit );

            data.imageSetter = imageSetter;
            data.removeReceived = removeProcessed;

            //these two are set for conveniance
            data.gameObject = gameObject.name;
            data.callback = ImagePickerReceive;

            ReceiveAllFiles( data );
        }

        /// <summary>
        /// Example callback from plugin, the Receive image callbacks add the index of the loaded file to the beginning of the filename with a | as a seperator
        /// </summary>
        /// <param name="value">the filename (or names) of the selected file</param>
        public void ImagePickerReceive( string result ) {
            int index = -1;
            string[] parts = result.Split( '|' );
            if( parts.Length != 2 || !int.TryParse( parts[0], out index ) ) {
                Debug.LogError( "Selected callback was not valid for the executed action" );
                if( processing.callbackError != null )
                    processing.callbackError.Invoke();
                return;
            }

            if( string.IsNullOrEmpty( parts[1] ) ) {
                //use logcat to get more information
                Debug.Log( "SDCard full or invalid file selected, please try again" );
                if( processing.callbackError != null )
                    processing.callbackError.Invoke();
                return;
            }

            if( processing.imageSetter == null ) {
                //it needs a texture before it can load
                if( processing.receiveImages == null || processing.receiveImages.Length <= index ) {
                    Debug.LogError( "Array not large enough to receive" );
                    if( processing.callbackError != null )
                        processing.callbackError.Invoke();
                    return;
                }

                if( processing.receiveImages[index] == null ) {
                    processing.receiveImages[index] = new Texture2D( 1, 1 );
                }

                //Debug.Log("between " + processing.receiveImages.Length + " vs " + index);

                //the bonus we get from using a lambda operator as an action is readable code 
                // also there is now no need pass a reference to the entry 
                // or even worse the entire array and a valid index

                StartCoroutine( LoadImage( parts[1], texture => processing.receiveImages[index] = texture ) );
            } else {
                StartCoroutine( LoadImage( parts[1], processing.imageSetter ) );
            }
        }

        /// <summary>
        /// Open an image from a known external location and cache it
        /// 
        /// This is usefull if you have a way of finding / selecting files within your Unity3D scenes
        /// And wish to process those files before allowing them to load.
        /// You could add these to the initial loading from the external cache to prevent problems when the User adds files manually to the directory.
        /// If those files are unsupported by Unity3D unexpected behaviour may occur, symptoms may include but are not limited to:
        /// - unable to load all images
        /// - chrashing the android video driver
        /// 
        /// Note:
        /// It is more efficient to load received files by index.
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filepath"></param>
        public void ProcessImage( ImagePickerData data, string filepath ) {
            processing = data;
            ajcImagePicker.CallStatic( "openFile", data.setting, filepath );
        }

        /// <summary>
        /// Share an image
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="menuTitle">title for the picker</param>
        internal void ShareFile( string filePath, string menuTitle ) {
            ajcImagePicker.CallStatic( "shareFile", filePath, menuTitle );
        }

        /// <summary>
        /// Share an image with a specific app
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="packageName">fully qualified name of the package (as received from getShareApps)</param>
        internal void ShareFileWithApp( string filePath, string packageName ) {
            ajcImagePicker.CallStatic( "shareFileWithApp", filePath, packageName );
        }

        /// <summary>
        /// Share a text using a picker
        /// </summary>
        /// <param name="text">text to share</param>
        /// <param name="subject">subject or title (optional depending on target)</param>
        /// <param name="menuTitle">title for the picker</param>
        internal void ShareText( string text, string subject, string menuTitle ) {
            ajcImagePicker.CallStatic( "shareText", text, subject, menuTitle );
        }
        /// <summary>
        /// Share a text using a picker
        /// </summary>
        /// <param name="text">text to share</param>
        /// <param name="subject">subject or title (optional depending on target)</param>
        /// <param name="packageName">fully qualified name of the package (as received from getShareApps)</param>
        internal void ShareTextWithApp( string text, string subject, string packageName ) {
            ajcImagePicker.CallStatic( "shareTextWithApp", text, subject, packageName );
        }

        /// <summary>
        /// Get all the apps we could share images with
        /// </summary>
        /// <returns>fully qualified package names</returns>
        internal string[] getShareApps() {
            return ajcImagePicker.CallStatic<string[]>( "getShareApps" );
        }

        /// <summary>
        /// Get all the apps we could share a file of <mimetype> with
        /// </summary>
        /// <param name="mimeType">mimeType like "image/*" or "video/*" </param>
        /// <returns>fully qualified package names</returns>
        internal string[] getShareApps( string mimeType ) {
            return ajcImagePicker.CallStatic<string[]>( "getShareApps", mimeType );
        }
    }
#elif UNITY_ANDROID && UNITY_EDITOR
    //the following code is for easier testing in the editor (calling the plugin in the editor does not work due to how Unity works).
#if UNITY_EDITOR
#warning Image Picker for Android, does not work in the Unity Editor
#else
#error Please make sure not to reference Android Image Picker in a non-Android build, you can then safely disable this error
#endif

    //TODO: all platform specific code should be removed for unsupported platforms, placeholder classes to handle failure while testing in editor
    public class ImagePicker : MonoBehaviour {

        private AndroidJavaClass imagePicker;

        /// <summary>
        /// set to higher then 0 before start
        /// 0 = silent
        /// 1 = errors only
        /// 2 = warnings & errors
        /// 3 = verbose 
        /// </summary>
        public int debug = 3;

        internal virtual void Awake() { }

        public string GetInternalDir() { return Application.persistentDataPath; }
        public string GetExternalDir() { return Application.persistentDataPath; }

        public void StartImagePicker( Texture2D image, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) { }
        public void StartImagePicker( Texture2D image, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) { }
        public void StartImagePicker( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false ) { }
    
        public void StartImagePicker( ImagePickerData data ) { }
        public void StartImagePicker( ImagePickerData data, GameObject callbackHandler, string function ) { }

        public void GetVideoPreview( ImagePickerData data, string videoPath ) { }

        public string GetRawEntry( int index ) { return null; }
        public int GetReceivedCount() { return 0; }
        public void RemoveReceivedEntry( int p ) { }

        public void ProcessReceived( Texture2D[] images, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool removeProcessed ) { }
        public void ProcessReceived( ImagePickerData data, bool removeProcessed ) { }

        public void ReceiveFile( ImagePickerData data, int index ) { }
        public void ReceiveFile( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit ) { }
        public void ReceiveAllFiles( System.Action<Texture2D> imageSetter, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool removeProcessed ) { }
        public void ReceiveAllFiles( Texture2D[] images, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool removeProcessed ) { }

        public int GetReceivedTextCount() { return 0; }

        public string[] GetReceivedText( int index ) { return null; }

        public void RemoveReceivedTextEntry( int index, int count = 1 ) { }

        public void ProcessImage( ImagePickerData data, string path ) { }

        public void ShareFile( string filePath, string menuTitle ) { }

        internal void ShareFileWithApp( string filePath, string packageName ) { }

        internal string[] getShareApps() { return new string[] { "Not.A.Real.App" }; }
        internal string[] getShareApps( string mimeType ) { return getShareApps(); }

        public virtual void ImagePickerCallback( string result ) { }
        public static IEnumerator LoadImage( string file, System.Action<Texture2D> imageSetter ) { yield return null; }
        public static IEnumerator LoadImage( string file, ImagePickerData data ) { yield return null; }
    }
    //TODO: though all platform specific code should be removed for unsupported platforms
#endif

}