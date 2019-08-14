var screenShotURL= "www.krazyidea.com/trivia_service/upload_file.php";
    
    function Start(){
          Debug.Log(screenShotURL);
    }
    
    function OnMouseDown() {
    
    // We should only read the screen after all rendering is complete
       yield WaitForEndOfFrame();
    
    // Create a texture the size of the screen, RGB24 format
    var width = Screen.width;
    var height = Screen.height;
    var tex = new Texture2D( width, height, TextureFormat.RGB24, false );

    // Read screen contents into the texture
    tex.ReadPixels( Rect(0, 0, width, height), 0, 0 );
    tex.Apply();
    
    // Encode texture into PNG
    var bytes = tex.EncodeToPNG();
    Destroy( tex );
    
    // Create a Web Form
    var form = new WWWForm();
    form.AddField("frameCount", Time.frameCount.ToString());
    form.AddBinaryData("file", bytes, "screenShot.png", "image/png");
    
    // Upload to a cgi script
    var w = WWW(screenShotURL, form);
    yield w;

        if (w.error != null){
             Debug.Log(w.error);
        }
         else{
               Debug.Log("Image Uploaded!");
          }
    }