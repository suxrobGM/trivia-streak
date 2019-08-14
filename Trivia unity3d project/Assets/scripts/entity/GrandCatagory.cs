using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GrandCatagory : MonoBehaviour {

	public GrandCatagoryData Data;
	public dfLabel menu_text;
	public dfWebSprite menu_icon;

	public void OnMouseDown()
	{
		CategoryConstant.GrandCategoryId = Data.th_cat_id;
		//Application.LoadLevel();
        SceneManager.LoadScene("TriviaGameSponsor");
	}

	public void start()
	{
		menu_text.Text = Data.th_cat_name;
		StartCoroutine(DownloadImg());
		//menu_icon.URL = util.getHttpGrandSubCategoryMediaPath()+Data.th_cat_image;
	}

	IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetHttpGrandSubCategoryMediaPath()+Data.th_cat_image;
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		menu_icon.Texture = www.texture as Texture;
	}
}
