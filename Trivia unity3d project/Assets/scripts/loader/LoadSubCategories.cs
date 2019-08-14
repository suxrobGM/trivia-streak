using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LoadSubCategories : MonoBehaviour {

	public SubCatagory Prefab;

	//public dfLabel topTitle;
	public dfPanel loaderPanel;
	public dfPanel notFoundPanel;
	
	// Use this for initialization
	void Start () {

		//topTitle.Text = categoryConstant.MainCategoryName;
		notFoundPanel.IsVisible = false;
		dfScrollPanel categoryPanel = GetComponent<dfScrollPanel> ();
		
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"second_categories.php?parent_id="+CategoryConstant.MainCategoryId)
			.OnReply((reply) => {
				
				SubCatagoryReply catReply = JsonConvert.DeserializeObject<SubCatagoryReply>(reply.DataAsString);
				if(catReply.success == 1){
					foreach(SubCatagoryData data in catReply.data)
					{
						
						SubCatagory instance  = categoryPanel.AddPrefab(Prefab.gameObject).GetComponent<SubCatagory>();
						
						//MainCatagory instance = Instantiate(Prefab) as MainCatagory;
						instance.Data = data;
						instance.start();
					}
					loaderPanel.IsVisible = false;
				}
				else{
				//	notFoundPanel.IsVisible = true;
				//	loaderPanel.IsVisible = false;

					CategoryConstant.SubCategoryId = 0;
					CategoryConstant.GrandCategoryId = 0;
					SceneManager.LoadScene("TriviaGameSponsor");

				}
			})
				.Send();
	}
	

}
