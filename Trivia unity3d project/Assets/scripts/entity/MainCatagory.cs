using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainCatagory : MonoBehaviour {

	public MainCatagoryData Data;
	public dfLabel menu_text;
	public dfWebSprite menu_icon;

	//public float screenWidth = Screen.width;

	public void OnMouseDown()
	{
		CategoryConstant.MainCategoryId = Data.cat_id;
		//categoryConstant.MainCategoryName = Data.cat_name;
		//SceneManager.LoadScene("TriviaSubCategory");
		SceneManager.LoadScene ("TriviaGameSponsor");
		
	}

	public void start()
	{
		//Debug.Log(screenWidth);
		//gameObject.GetComponent<dfPanel>().Width = screenWidth;
		//gameObject.renderer.bounds.size.x = screenWidth;
		menu_text.Text = Data.cat_name;
		StartCoroutine(DownloadImg());
	}

	
	IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetHttpMainCategoryMediaPath()+Data.cat_image;
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		menu_icon.Texture = www.texture as Texture;
	}
}
