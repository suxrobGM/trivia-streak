using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class updateuserpass : MonoBehaviour {

	public dfTextbox passText;
	public dfLabel invalidText;
	public AudioClip clickBtn;

	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		if(!passText.Text.ToString().Equals(""))
		{
			HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
			
			new HTTP.Request(TriviaService.GetHttpFolderPath()+"update_pass.php?user_id="+GameConstant.UserId+"&user_pass="+passText.Text.ToString())
				.OnReply((reply) => {
					
					UpdateReply updateReply = JsonConvert.DeserializeObject<UpdateReply>(reply.DataAsString);
					if(updateReply.success == 1){
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
					}
					else{
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
					}
				})
					.Send();
		}
		else{
			invalidText.Text = "Pass not valid.";
			invalidText.IsVisible = true;
		}
		
	}

}
