using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubCatagory : MonoBehaviour {

	public SubCatagoryData Data;
	public dfLabel menu_text;
	public dfWebSprite menu_icon;

	public void OnMouseDown()
	{
		CategoryConstant.SubCategoryId = Data.sec_cat_id;
		//categoryConstant.SubCategoryName = Data.sec_cat_name;
		SceneManager.LoadScene("TriviaGrandCategory");
	}

	public void start()
	{
		menu_text.Text = Data.sec_cat_name;
		StartCoroutine(DownloadImg());
		//menu_icon.URL = util.getHttpSubCategoryMediaPath()+Data.sec_cat_image;
	}
	IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetHttpSubCategoryMediaPath()+Data.sec_cat_image;
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		menu_icon.Texture = www.texture as Texture;
	}
}
