using UnityEngine;
using System.Collections;

public class LeaderBoard : MonoBehaviour {

	public LeaderBoardData Data;
	public dfLabel menu_text;
	public dfLabel menu_points;
	public dfLabel menu_serial;
	public dfWebSprite menu_icon;
	public static int serial = 1;

	public void start()
	{
		menu_text.Text = Data.game_user_id;
		menu_points.Text = Data.game_point.ToString();
		menu_serial.Text = serial.ToString();
		serial++;
		StartCoroutine(DownloadImg());
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
