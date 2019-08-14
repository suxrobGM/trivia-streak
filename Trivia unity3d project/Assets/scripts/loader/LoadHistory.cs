using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadHistory : MonoBehaviour {
	
	public History Prefab;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	void Start ()
	{
		
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));		
		
		notFoundPanel.IsVisible = false;
		dfScrollPanel historyPanel = GetComponent<dfScrollPanel> ();
		//categoryPanel.Anchor = dfAnchorStyle.All;
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_user_game_history.php?game_user_id="+GameConstant.UserId+"&game_user_type="+GameConstant.UserType)
			.OnReply((reply) => {
				
				HistoryReply hReply = JsonConvert.DeserializeObject<HistoryReply>(reply.DataAsString);
				if(hReply.success == 1){
					foreach(HistoryData data in hReply.data)
					{
						
						History instance  = historyPanel.AddPrefab(Prefab.gameObject).GetComponent<History>();
						
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
