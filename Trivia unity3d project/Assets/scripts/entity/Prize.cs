using UnityEngine;
using System.Collections;

public class Prize : MonoBehaviour {

	public PrizeData Data;
	public dfLabel menu_text;
	public dfLabel date_text;
	public dfLabel score_text;
	public dfWebSprite menu_icon;

	public dfTextureSprite playBtn;
	public dfTextureSprite leaderBtn;

	public void start()
	{

		leaderBtn.GetComponent<goToTriviaLeaderBoard>().prizeID = Data.prize_id;
		playBtn.GetComponent<goToTriviaPlayCategory>().prizeID = Data.prize_id;
		playBtn.GetComponent<goToTriviaPlayCategory>().prizeTopScore = Data.score;

		menu_text.Text = Data.prize_title;
		date_text.Text = "Expires: "+Data.prize_date.ToString();
		score_text.Text = Data.score.ToString();
		StartCoroutine(DownloadImg());
	}

	IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetHttpPrizeMediaPath()+Data.prize_image;
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		menu_icon.Texture = www.texture as Texture;
	}
}
