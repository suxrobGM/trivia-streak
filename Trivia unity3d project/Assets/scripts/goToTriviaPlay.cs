using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class goToTriviaPlay : MonoBehaviour {


	public dfTextbox emailText;
	public dfTextbox passText;
	public dfLabel invalidText;
	//private string url = "login_user.php";
	//?app_user_name=saqib&app_user_pass=1234";
	public AudioClip clickBtn;
	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

        if (!emailText.Text.ToString().Equals("") && !passText.Text.ToString().Equals(""))
        {
            HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));

            new HTTP.Request(TriviaService.GetHttpFolderPath() + "login_user.php?user_name=" + emailText.Text.ToString() + "&user_pass=" + passText.Text.ToString())
            .OnReply((reply) => {

                LoginReply loginReply = JsonConvert.DeserializeObject<LoginReply>(reply.DataAsString);
                if (loginReply.success == 1)
                {
                    GameConstant.UserId = emailText.Text.ToString();
                    GameConstant.UserType = "OWN";
                    GameConstant.UserEmail = loginReply.data[0].user_email;
                    GameConstant.UserCountry = loginReply.data[0].user_country;
                    GameConstant.IsMusic = loginReply.data[0].ismusic;
                    GameConstant.IsSound = loginReply.data[0].issound;
                    //Debug.Log(gameConsatant.user_country);
                    SceneManager.LoadScene("TriviaGamePlay");
                }
                else
                {
                    invalidText.IsVisible = true;
                }
            })
            .Send();
        }

    }

}
