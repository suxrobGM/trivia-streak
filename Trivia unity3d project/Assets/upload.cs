using UnityEngine;
using System.Collections;
public class upload : MonoBehaviour {

	public dfTextureSprite text;
	void Start(){
		//yield return new WaitForEndOfFrame();

		// Create a texture the size of the screen, RGB24 format
		//int width = Screen.width;
		//int height = Screen.height;
		//Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		
		// Read screen contents into the texture
		//tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
		//tex.Apply();
		Texture2D tex = text.Texture as Texture2D;
		StartCoroutine(UploadImg(tex));
	}

	IEnumerator UploadImg(Texture2D texture)
	{
		string screenShotURL = "www.krazyidea.com/trivia_service/upload_file.php";
		byte[] bytes = texture.EncodeToPNG();
		Destroy( texture );
		// Create a Web Form
		WWWForm form = new WWWForm();
		form.AddField("frameCount", Time.frameCount.ToString());
		form.AddBinaryData("file", bytes, "Image.png", "image/png");
		// Upload to a cgi script
		WWW w = new WWW(screenShotURL, form);
		yield return w;
		if (w.error != null){
			print(w.error);
			Application.ExternalCall( "debug", w.error);
			//print(screenShotURL);
		}
		else{
			print("Finished Uploading Screenshot");
			//print(screenShotURL);
			//image = data.loadImage;
			Application.ExternalCall( "debug", "Finished Uploading Screenshot");
		}
	}	
}
