using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour {

	
	public WinnerData Data;
	public dfLabel winner_name;
	public dfLabel prize_name;
	public dfLabel score_text;
	public dfLabel prize_date;
	public dfWebSprite menu_icon;

	public void start()
	{
			
		winner_name.Text = Data.game_user_id;
		prize_name.Text = Data.prize_title;
		score_text.Text = Data.winner_points.ToString();
		prize_date.Text = Data.prize_date;
		StartCoroutine(DownloadImg());
		//menu_icon.URL = util.getHttpPrizeMediaPath()+Data.prize_image;
		//menu_icon.AutoDownload = true;
		//menu_icon.LoadImage();
	}

	IEnumerator DownloadImg ()
	{
		string url = "http://www.krazyidea.com/trivia_service/profile_images/"+Data.game_user_id+"_profile.png";
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		menu_icon.Texture = www.texture as Texture;
	}
}
