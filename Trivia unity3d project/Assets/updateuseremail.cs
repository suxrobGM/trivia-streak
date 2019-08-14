using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class updateuseremail : MonoBehaviour {
	public dfTextbox emailText;
	public dfLabel invalidText;
	public AudioClip clickBtn;

	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		if(!emailText.Text.ToString().Equals(""))
		{
			HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
			
			new HTTP.Request(TriviaService.GetHttpFolderPath()+"update_email.php?user_id="+GameConstant.UserId+"&user_email="+emailText.Text.ToString())
				.OnReply((reply) => {
					
					UpdateReply updateReply = JsonConvert.DeserializeObject<UpdateReply>(reply.DataAsString);
					if(updateReply.success == 1){
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
						GameConstant.UserEmail = emailText.Text.ToString();
					}
					else{
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
					}
				})
					.Send();
		}
		else{
			invalidText.Text = "Email not valid.";
			invalidText.IsVisible = true;
		}

	}
}
