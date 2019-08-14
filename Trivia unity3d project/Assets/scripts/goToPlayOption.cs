using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class goToPlayOption : MonoBehaviour {

	//public string gameType;

	//public static PointReply pointReply;
	public AudioClip clickBtn;
	public dfTextureSprite mainpanel;
	public dfSpriteAnimation progress;
	public static PointReply pointReply;
	public void OnClick()
	{
		CategoryConstant.GameType = "fun";
		AudioSource backgroundClip =  mainpanel.GetComponent<AudioSource>();

		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();

		if (!backgroundClip.isPlaying)
		{
			backgroundClip.Play();
			progress.Resume();
		}
        else {
			backgroundClip.Stop();
			progress.Stop();
            //  start game play

            if (CategoryConstant.GameType.Equals("fun"))
            {
                HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));

                new HTTP.Request(TriviaService.GetHttpFolderPath() + "get_player_fun_points.php?prize_id=" + CategoryConstant.PrizeId + "&th_cat_id=" + CategoryConstant.MainCategoryId + "&sec_cat_id=" + CategoryConstant.SubCategoryId + "&cat_id=" + CategoryConstant.GrandCategoryId + "&game_user_id=" + GameConstant.UserId + "&game_user_type=" + GameConstant.UserType)
                    .OnReply((reply) => {

                        pointReply = JsonConvert.DeserializeObject<PointReply>(reply.DataAsString);

                        if (pointReply.success == 1)
                        {
                            GameConstant.FunHighScore = pointReply.points;
                        }
                        else
                        {
                            GameConstant.FunHighScore = 0;
                        }

                    }).Send();
            }

            GameConstant.CurrentScore = 0;
            SceneManager.LoadScene("TriviaStratPlay");
        }
	}
}
