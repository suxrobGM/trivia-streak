using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadLeaderboard : MonoBehaviour {

	
	public LeaderBoard Prefab;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	void Start ()
	{
		notFoundPanel.IsVisible = false;
		dfScrollPanel leaderPanel = GetComponent<dfScrollPanel> ();
		//categoryPanel.Anchor = dfAnchorStyle.All;
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_game_leaderboard.php?prize_id="+CategoryConstant.PrizeId)
			.OnReply((reply) => {
				
				LeaderBoardReply leaderReply = JsonConvert.DeserializeObject<LeaderBoardReply>(reply.DataAsString);
				if(leaderReply.success == 1){
					foreach(LeaderBoardData data in leaderReply.data)
					{
						
						LeaderBoard instance  = leaderPanel.AddPrefab(Prefab.gameObject).GetComponent<LeaderBoard>();
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
