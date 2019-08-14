using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class goToIntroduction : MonoBehaviour {
	public dfTweenFloat Tween;

	public dfTextbox emailText;
	public dfTextbox passText;

	//private string url = "login_user.php";
	//?app_user_name=saqib&app_user_pass=1234";

	public void OnClick()
	{
		if(!emailText.Text.ToString().Equals("") && !passText.Text.ToString().Equals(""))
		{
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
			new HTTP.Request(TriviaService.GetHttpFolderPath()+"login_user.php?user_name="+emailText.Text.ToString()+"&user_pass="+passText.Text.ToString())
			.OnReply((reply) => {
				
					LoginReply loginReply = JsonConvert.DeserializeObject<LoginReply>(reply.DataAsString);
				if(loginReply.success == 1){
						GameConstant.UserId = emailText.Text.ToString();
						GameConstant.UserType = "OWN";

						Tween.Play ();
						Tween.TweenCompleted += (dfTweenPlayableBase sender) => {
							SceneManager.LoadScene ("introduction");
						};
				}
				else{
					
				}
			})
				.Send();


		}

	}

}
