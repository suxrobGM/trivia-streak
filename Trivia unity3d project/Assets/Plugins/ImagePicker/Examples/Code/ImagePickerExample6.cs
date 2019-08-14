#if UNITY_ANDROID
using UnityEngine;
using System.Collections;
using ElicitIce;
using System.IO;

/// <summary>
/// Please note that the implementation is not complete yet in version 9
/// Send me an e-mail if you would like development to continue on this feature
/// 
/// Due to how sharing works, the write SD card permission must be enabled in the manifest for the code to work.
/// 
/// </summary>

public class ImagePickerExample6 : MonoBehaviour {

    public ImagePicker picker;
    ImagePickerData data;
    public Texture2D share;
    public string shareFile = null;
    public string[] shareApps = null;

    public string[] mimeTypes = new string[]{
        "image/*",
        "video/*"
    };
    int selectedMimeType = 0; // 0 image, 1 video, others: not implemented
    int receivedMimeType = 0;

    int selected = 0;

    public string SaveTexture( Texture2D save ) {
        var bytes = save.EncodeToPNG();
        string file = save.name == null ? "defaultName" : save.name;
        string path = picker.GetExternalDir() + "/" + file + ".png";
        File.WriteAllBytes( path, bytes );
        return path;
    }

    public void ShareFile( string filePath, string menuTitle ) {
        picker.ShareFile( filePath, menuTitle );
    }

    public void ShareFileWithApp( string filePath, string packageName ) {
        picker.ShareFileWithApp( filePath, packageName );
    }

    public string[] GetShareApps() {
        return picker.getShareApps( mimeTypes[selectedMimeType] );
    }

    // Use this for initialization
    void Start() {
        SetupPicker();
        shareFile = SaveTexture( share );
    }

    void SetupPicker() {
        if( picker == null )
            picker = SmartMonoBehaviour.FindOrCreate<ImagePicker>();

        data = new ImagePickerData();
        data.gameObject = name;
        data.callback = ReceiveFilename;
        data.showCamera = true;
    }

    /// <summary>
    /// Customized callback so we get more control
    /// </summary>
    /// <param name="value">the filename (or names) of the selected file</param>
    public virtual void ReceiveFilename( string result ) {
        if( string.IsNullOrEmpty( result ) ) {
            //use logcat to get more information
            Debug.Log( "Result was empty, check logcat" );
            if( data.callbackError != null )
                data.callbackError.Invoke();
            return;
        }

        if( data.imageSetter == null )
            SetFileName( result );
        else
            StartCoroutine( ImagePicker.LoadImage( result, data.imageSetter ) );
    }

    private void OpenMimeTypePicker() {
        data.fileType = mimeTypes[selectedMimeType];
        data.imageSetter = null;
        picker.StartImagePicker( data );
        //reset the share app mimetype
        GetShareApps();
    }

    private void SetFileName( string path ) {
        receivedMimeType = selectedMimeType;
        shareFile = path;
        data.imageSetter = x => share = x;
        switch( selectedMimeType ) {
            case 1: //video
                //load a preview
                picker.GetVideoPreview( data, shareFile );
                break;
            case 0: //image
                StartCoroutine( ImagePicker.LoadImage( shareFile, data.imageSetter ) );
                break;
            default:
                Debug.LogWarning( "Implement your mimetype handling here" );
                break;
        }
    }

    // Update is called once per frame
    void OnGUI() {
        GUILayoutOption[] btnOpts= new GUILayoutOption[]{
            GUILayout.Height(Screen.height * 0.12f)
        };

        int newMime = GUILayout.SelectionGrid( selectedMimeType, mimeTypes, 5, btnOpts[0], GUILayout.Width( Screen.width * 0.75f ) );
        if( selectedMimeType != newMime ) {
            shareApps = GetShareApps(); //"Get all apps we can share " + mimeTypes[selectedMimeType] +" with"
            selectedMimeType = newMime;
        }

        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical( GUILayout.Width( Screen.width * 0.5f ) );
            {

                if( GUILayout.Button( "Open Picker for " + mimeTypes[selectedMimeType], btnOpts ) ) {
                    OpenMimeTypePicker();
                }
                if( !string.IsNullOrEmpty( shareFile ) ) {
                    if( shareApps == null || shareApps.Length == 0 ) {
                        if( GUILayout.Button( "Get all apps we can share " + mimeTypes[selectedMimeType] + " with", btnOpts ) ) {
                            shareApps = GetShareApps();
                        }
                    } else {
                        selected = (int)GUILayout.HorizontalSlider( selected, 0, shareApps.Length - 1, btnOpts );

                        if( selected > shareApps.Length )
                            selected = shareApps.Length - 1;
                        if( selected < 0 )
                            selected = 0;

                        if( GUILayout.Button( "Share: " + shareFile + "\nwith " + shareApps[selected], btnOpts ) ) {
                            ShareFileWithApp( shareFile, shareApps[selected] );
                        }
                    }
                    if( GUILayout.Button( "Share: " + shareFile, btnOpts ) ) {
                        ShareFile( shareFile, "Share With ....." );
                    }

                    if( receivedMimeType == 1 && GUILayout.Button( "Play video " + shareFile, btnOpts ) ) {
                        StartCoroutine( _PlayMovie() );
                    }
                }
            } GUILayout.EndVertical();
            GUILayout.BeginVertical( GUILayout.Width( Screen.width * 0.5f ) );
            {
                //GUILayout.Space( 0.1f * Screen.height );
                if( GUILayout.Button( share, GUI.skin.label ) )
                    shareFile = SaveTexture( share );
                if( string.IsNullOrEmpty( shareFile ) )
                    GUILayout.Label( "Touch the image to save it" );
                else
                    GUILayout.Label( shareFile );
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }

    protected IEnumerator _PlayMovie() {
        yield return Handheld.PlayFullScreenMovie( shareFile, Color.black, FullScreenMovieControlMode.CancelOnInput );
        if( Input.GetKey( KeyCode.Escape ) ) {

        }
        yield return new WaitForSeconds( 2 );
    }
}

#else
#warning ImagePickerExamples contain no code on non-Android Platform
#endif