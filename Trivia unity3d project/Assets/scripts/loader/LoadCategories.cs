using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LoadCategories : MonoBehaviour {

	public MainCatagory Prefab;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	void Start ()
	{
		
		//  Grab user current daily game
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_player_game.php?game_user_id="+GameConstant.UserId+"&game_user_type="+GameConstant.UserType)
			.OnReply((reply2) => {
				
				DailyGameReply dailyReply = JsonConvert.DeserializeObject<DailyGameReply>(reply2.DataAsString);
				if(dailyReply.daily_game == 0){
					SceneManager.LoadScene ("TriviaInAppPurchase");
				}
			}).Send();


		notFoundPanel.IsVisible = false;
		dfScrollPanel categoryPanel = GetComponent<dfScrollPanel> ();
		//categoryPanel.Anchor = dfAnchorStyle.All;
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"first_categories.php")
			.OnReply((reply) => {

				MainCatagoryReply catReply = JsonConvert.DeserializeObject<MainCatagoryReply>(reply.DataAsString);
				if(catReply.success == 1){
					foreach(MainCatagoryData data in catReply.data)
					{

						MainCatagory instance  = categoryPanel.AddPrefab(Prefab.gameObject).GetComponent<MainCatagory>();
						//instance.GetComponent<dfPanel>().Anchor = dfAnchorStyle.All;

						instance.Data = data;
						instance.start();
					}
					loaderPanel.IsVisible = false;
				}
				else{
					notFoundPanel.IsVisible = true;
					loaderPanel.IsVisible = false;
				}
			})
				.Send();


	}
}
