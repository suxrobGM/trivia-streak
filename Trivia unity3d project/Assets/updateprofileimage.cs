using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class updateprofileimage : MonoBehaviour {

	public dfTextureSprite icon;
	public dfLabel alert_text;
	public AudioClip clickBtn;

	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		StartCoroutine(UploadImg(GameConstant.UserId.ToString()));
	}


	IEnumerator UploadImg(string user_name)
	{
		Texture2D texture = icon.Texture as Texture2D;
		string screenShotURL = TriviaService.GetImageUploadUrl ();
		byte[] bytes = texture.EncodeToPNG();
		//Destroy( texture );
		// Create a Web Form
		string name = user_name+"_profile.jpg";
		WWWForm form = new WWWForm();
		form.AddField("frameCount", Time.frameCount.ToString());
		//form.AddField("user_id",  gameConsatant.user_id);
		form.AddBinaryData("file", bytes, name, "image/jpg");
		// Upload to a cgi script
		UnityWebRequest www = UnityWebRequest.Post(screenShotURL, form);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
							alert_text.Text = "Image successfully uploaded.";
							alert_text.IsVisible = true;
		}

	}
	
}
