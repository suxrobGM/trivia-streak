// ImagePickerData.cs is the main interface to supply the plugin your parameters
using UnityEngine;

#if UNITY_ANDROID || DEBUG
namespace ElicitIce {
    //This delegate is used to identify a function name, without having to turn it into a string
    //Less typo's this way, please note: the callback is sent as a string and is not compatible with lambda or similar operations.
    //Just create a callback function that matches this prototype, slap it on a gameobject and it is good to go.
    public delegate void ImageResultFunc(string pathToImg);

    /// <summary>
    /// Data class to store settings for the Image Picker
    /// </summary>
    public class ImagePickerData {
        public AndroidJavaObject setting = new AndroidJavaObject("com.ElicitIce.Plugin.ImagePickerData");

        /// <summary>
        /// Preferred constructor, see example 1, 2 and 5 on how to set up the data
        /// </summary>
        public ImagePickerData() { }

        /// <summary>
        /// Basic parameters constructor
        /// </summary>
        /// <param name="image">a non-null image to load the new texture into</param>
        /// <param name="fileName">filename to save the selected image into</param>
        /// <param name="subDir">subdir to save the selected image into</param>
        /// <param name="maxWidth">maximum width the image will be downscaled to</param>
        /// <param name="maxHeight">maximum height the image will be downscaled to</param>
        /// <param name="bestFit">false: simple downscale, true: downscale to either maxWidth or maxHeight where both are below max</param>
        /// <param name="showCamera">true: add Camera apps to picker, false: no camera apps</param>
        public ImagePickerData(Texture2D image, string fileName, string subDir, int maxWidth, int maxHeight, bool bestFit, bool showCamera = false) {
            this.loadImage = image;
            this.bestFit = bestFit;
            this.showCamera = showCamera;
            this.fileName = fileName;
            this.fileSubDir = subDir;
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
        }

        #region ImagePicker Default Implementation variables
        /// <summary>
        /// Replace this image with the new image, you can assign the material before or after it works either way
        /// </summary>
        public Texture2D loadImage = null;
        /// <summary>
        /// Array of images to handle receiving images, for best performance resize this to the amount of images expected or higher
        /// </summary>
        public Texture2D[] receiveImages = null;
        /// <summary>
        /// Action to take, with the new texture as a result, leave null to use loadImage
        /// </summary>
        public System.Action<Texture2D> imageSetter = null;
        /// <summary>
        /// Optional, leave null if not used
        /// a function to call when a callback returned but received an invalid result (null, file not found or wrong format)
        /// </summary>
        public System.Action callbackError = null;
        #endregion ImagePicker Default Implementation variables

        /// <summary>
        /// Optional sub directory, the plugin will convert \ to / and append them when either is missing, default: "/" 
        /// </summary>
        public string fileSubDir {
            set { setting.Set<string>("fileSubDir", value); }
        }

        /// <summary>
        /// Optional file name, if null: use filename of the original file
        /// Once the file picker is used, it becomes your responsibilty to remove unused files
        /// Storing filenames somewhere for easy access is recommended.
        /// </summary>
        public string fileName {
            set { setting.Set<string>("fileName", value); }
        }

        /// <summary>
        /// set the Max Height the resized image may have. Must be set to > 0 otherwise 1024 is chosen
        /// </summary>
        public int maxHeight {
            set { setting.Set<int>("maxH", value); }
        }

        /// <summary>
        /// set the Max width the resized image may have. Must be set to > 0 otherwise 1024 is chosen
        /// </summary>
        public int maxWidth {
            set { setting.Set<int>("maxW", value); }
        }

        /// <summary>
        /// 0 = auto
        /// unsupported: 1 = Select Image without best fit (overrides bestFit)
        /// unsupported: 2 = Select Image Bestfit (overrides bestFit)
        /// unsupported: 3 = Select File, returns path but only for standard file pickers (you need to also set the mime type in type)
        /// unsupported: 4 = Select RAW path, does not change or read the file, use the function: static void openFile(ImagePickerData data, String rawpath); to get a cached version manually
        /// </summary>
        public int mode {
            set { setting.Set<int>("mode", value); }
        }

        /// <summary>
        /// FALSE:
        /// scales down by 2,4,8,16,etc. (power of two) to the first values below maxWidth and maxHeight
        /// example: 
        /// max 1024 x 1024, image selected 1244 x 128,
        /// results in an image with dimensions: 622 x 64
        /// 
        /// TRUE
        /// scale down to the first power of two division that is not less then the maximum dimensions
        /// then use aspect correct scaling to constrain to match the largest image size within the desired dimensions
        /// note, requires more calculations and uses a LOT more memory.
        /// Using a larger maximum texture size with bestFit=false uses less memory then loading a smaller maximum size with bestFit:true.
        /// 
        /// bestFit uses more memory when used, but returns an image that match at least one axis to the requested maximum image.
        /// The amount of memory used is proportional to the requested maximum dimensions, requesting smaller textures uses less memory.
        /// If memory usage is still an issue, please lower the resolution when using bestFit, or disable bestFit and request a higher resolution image.
        /// 
        /// example:
        /// max 1024 x 1024, image selected 1244 x 128,
        /// results in an image with dimensions: 1024 x 105
        /// </summary>
        public bool bestFit {
            set { setting.Set<bool>("bestFit", value); }
        }

        /// <summary>
        /// Show Camera apps in the Image picker list
        /// 
        /// TRUE: Shows all available camera apps
        /// 
        /// <!-- The following permissions turn out to be optional, include them if you want to prevent users from downloading the app if they have no camera -->
        /// <uses-permission android:name="android.permission.CAMERA" />
        /// <uses-feature android:name="android.hardware.camera" />
        /// <uses-feature android:name="android.hardware.camera.autofocus" />
        /// 
        /// FALSE: No camera is added, no additional permissions required
        /// </summary>
        public bool showCamera {
            set { setting.Set<bool>("showCamera", value); }
        }

        /// <summary>       
        /// Attempt to open the picker or camera using the set default app        
        /// showCamera true:  Camera apps
        /// showCamera false: Gallery apps
        /// 
        /// if none is available or set, open an App chooser for the selected type of app
        /// this dialog also allows the user to set a default
        /// </summary>
        public bool useDefault {
            set { setting.Set<bool>("useDefault", value); }
        }

        /// <summary>
        /// Allow Image Picker apps to return multiple Images.
        /// These are sent as though they were shared (via the received Images)
        /// The benefit of this method is that the user does not have to 
        /// manually switch to another app before picking the images.
        /// 
        /// This was added to the official Android API in version 18, 
        /// but it might work on older Androids if the apps support it.
        /// </summary>
        public bool selectMultiple {
            set { setting.Set<bool>( "selectMultiple", value ); }
        }

        /// <summary>
        /// Set JPEG quality from 0 (maximum quality) to 100 (best image quality)
        /// if quality < 0, all images will be stored as PNG, regardless of the original type
        /// </summary>
        public int quality {
            set { setting.Set<int>("compression", value); }
        }

        /// <summary>
        /// The name of the gameobject to send the callback to
        /// </summary>
        public string gameObject {
            set { setting.Set<string>("gameobj", value); }
        }

        /// <summary>
        /// Delegate callback function
        /// </summary>
        public ImageResultFunc callback {
            set { setting.Set<string>("callback", value.Method.Name); }
        }

        /// <summary>
        /// Delegate callback function, returns an internal status code.
        /// Due to the way Unity works a string was required.
        /// </summary>
        public ImageResultFunc callbackOnCancel {
            set { setting.Set<string>("callbackCancel", value.Method.Name); }
        }

        /// <summary>
        /// Get the logcat information sent by the plugin, only the Verbose
        /// </summary>
        public ImageResultFunc debugVerbose {
            set { setting.Set<string>("callbackVerbose", value.Method.Name); }
        }

        /// <summary>
        /// Get the logcat information sent by the plugin, only the Warnings
        /// </summary>
        public ImageResultFunc debugWarning {
            set { setting.Set<string>("callbackWarning", value.Method.Name); }
        }

        /// <summary>
        /// Get the logcat information sent by the plugin, only the Errors
        /// </summary>
        public ImageResultFunc debugError {
            set { setting.Set<string>("callbackError", value.Method.Name); }
        }

        /// <summary>
        /// Optional override of android system mime type, could be used to change ImagePicker into a basic file picker
        /// defaults to: image/*
        /// </summary>
        public string fileType {
            set { setting.Set<string>("type", value); }
        }

        /// <summary>
        /// Optional override of the Image Picker title, this is the title in the smaller window.
        /// To change the title in the full screen window, change the label in the manifest.
        /// </summary>
        public string DialogTitle {
            set { setting.Set<string>("dialog", value); }
        }

        /// <summary>
        /// If true all images requested through processReceived will be removed
        /// set this to false if you want to process the list twice (perhaps to get smaller thumbnails first)
        /// </summary>
        public bool removeReceived { set { setting.Set<bool>("removeReceived", value); } }
    }
}
#endif
