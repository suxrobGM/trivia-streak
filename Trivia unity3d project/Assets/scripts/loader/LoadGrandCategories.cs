using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LoadGrandCategories : MonoBehaviour {

	public GrandCatagory Prefab;

	//public dfLabel topTitle;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	
	// Use this for initialization
	void Start () {

		//topTitle.Text = categoryConstant.SubCategoryName;
		notFoundPanel.IsVisible = false;
		dfScrollPanel categoryPanel = GetComponent<dfScrollPanel> ();
		
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"third_categories.php?parent_id="+CategoryConstant.SubCategoryId+"&grand_parent_id="+CategoryConstant.MainCategoryId)
			.OnReply((reply) => {
				
				GrandCatagoryReply catReply = JsonConvert.DeserializeObject<GrandCatagoryReply>(reply.DataAsString);
				if(catReply.success == 1){
					foreach(GrandCatagoryData data in catReply.data)
					{
						
						GrandCatagory instance  = categoryPanel.AddPrefab(Prefab.gameObject).GetComponent<GrandCatagory>();

						instance.Data = data;
						instance.start();
					}
					loaderPanel.IsVisible = false;
				}
				else{
					//notFoundPanel.IsVisible = true;
					//loaderPanel.IsVisible = false;

					CategoryConstant.GrandCategoryId = 0;
					SceneManager.LoadScene("TriviaGameSponsor");
				}
			})
				.Send();
	}
	

}
