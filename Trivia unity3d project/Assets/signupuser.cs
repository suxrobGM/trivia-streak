using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class signupuser : MonoBehaviour {

	public dfTextbox usernameText;
	public dfTextbox emailText;
	public dfTextbox passText;
	public dfTextbox rpassText;
	public dfLabel invalidText;
	public dfTextureSprite icon;
	//private string url = "login_user.php";
	//?app_user_name=saqib&app_user_pass=1234";
	public AudioClip clickBtn;
	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		if (!emailText.Text.ToString().Equals("email") && !usernameText.Text.ToString().Equals("username"))
		{
			if (!emailText.Text.ToString().Equals("") && !passText.Text.ToString().Equals("") && !usernameText.Text.ToString().Equals("") && !rpassText.Text.ToString().Equals(""))
			{
				if (passText.Text.ToString().Equals(rpassText.Text.ToString()))
                {
				    HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
				
					new HTTP.Request(TriviaService.GetHttpFolderPath()+"register_user.php?user_name="+usernameText.Text.ToString()+"&user_pass="+passText.Text.ToString()+"&user_email="+emailText.Text.ToString()+"&user_type=OWN")
					.OnReply(reply => 
                    {
						
						RegisterReply registerReply = JsonConvert.DeserializeObject<RegisterReply>(reply.DataAsString);
						if (registerReply.success == 1)
                        {
							GameConstant.UserId = usernameText.Text.ToString();
							GameConstant.UserType = "OWN";
							GameConstant.UserEmail = emailText.Text.ToString();							
							StartCoroutine(UploadImg(usernameText.Text.ToString()));																		
						}
						else {
							invalidText.Text = registerReply.message.ToString();
							invalidText.IsVisible = true;
						}
					})
                    .Send();
				}
                else {
					invalidText.Text = "Password not match.";
					invalidText.IsVisible = true;
				}				
			}
            else {
				invalidText.Text = "Fill all the required fields.";
				invalidText.IsVisible = true;
			}
		}
        else {
			invalidText.Text = "Fill all the required fields.";
			invalidText.IsVisible = true;
		}
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

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
			SceneManager.LoadScene("TriviaSetting");
		}
	}	
}
