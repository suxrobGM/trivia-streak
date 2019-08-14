using UnityEngine;
using System.Collections;

public class History : MonoBehaviour {

	public HistoryData Data;
	public dfLabel menu_title;
	public dfWebSprite menu_icon;
	public dfLabel menu_date;
	public dfLabel menu_score;
	public dfLabel menu_rank;
	

	public void start()
	{
		menu_title.Text = Data.prize_title;
		menu_date.Text = "Ended on "+Data.prize_date;
		menu_score.Text = Data.score.ToString();
		menu_rank.Text = Data.rank.ToString();


	}
}
