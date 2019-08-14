using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadWinner : MonoBehaviour {

	public Winner Prefab;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	void Start ()
	{

		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));		
		
		notFoundPanel.IsVisible = false;
		dfScrollPanel winnerPanel = GetComponent<dfScrollPanel> ();
		//categoryPanel.Anchor = dfAnchorStyle.All;
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_game_winners.php")
			.OnReply(reply => 
            {
				
				WinnerReply winReply = JsonConvert.DeserializeObject<WinnerReply>(reply.DataAsString);
				if(winReply.success == 1){
					foreach(WinnerData data in winReply.data)
					{
						
						Winner instance  = winnerPanel.AddPrefab(Prefab.gameObject).GetComponent<Winner>();
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
