using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class updateusercountry : MonoBehaviour {

	public dfDropdown countryText;
	public dfLabel invalidText;
	public AudioClip clickBtn;



	public void OnSelectedIndexChanged()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		if(!countryText.SelectedValue.ToString().Equals("Select Country"))
		{
			HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
			
			new HTTP.Request(TriviaService.GetHttpFolderPath()+"update_country.php?user_id="+GameConstant.UserId+"&user_country="+countryText.SelectedValue.ToString())
				.OnReply((reply) => {
					
					UpdateReply updateReply = JsonConvert.DeserializeObject<UpdateReply>(reply.DataAsString);
					if(updateReply.success == 1){
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
						GameConstant.UserCountry = countryText.SelectedValue.ToString();
					}
					else{
						invalidText.Text = updateReply.message.ToString();
						invalidText.IsVisible = true;
					}
				})
					.Send();
		}
		else{
			invalidText.Text = "Country not valid.";
			invalidText.IsVisible = true;
		}
		
	}
}
