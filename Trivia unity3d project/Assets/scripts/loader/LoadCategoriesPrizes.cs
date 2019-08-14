using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LoadCategoriesPrizes : MonoBehaviour {

	public Prize Prefab;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	void Start ()
	{
		notFoundPanel.IsVisible = false;
		dfScrollPanel prizePanel = GetComponent<dfScrollPanel> ();
		//categoryPanel.Anchor = dfAnchorStyle.All;
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"prize.php")
			.OnReply((reply) => {

				PrizeReply catReply = JsonConvert.DeserializeObject<PrizeReply>(reply.DataAsString);
				if(catReply.success == 1){
					foreach(PrizeData data in catReply.data)
					{

						Prize instance  = prizePanel.AddPrefab(Prefab.gameObject).GetComponent<Prize>();
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
