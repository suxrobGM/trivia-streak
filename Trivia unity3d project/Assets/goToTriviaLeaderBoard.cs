using UnityEngine;
using UnityEngine.SceneManagement;

public class goToTriviaLeaderBoard : MonoBehaviour {

	public int prizeID;

	public void OnMouseDown()
	{
		//categoryConstant.MainCategoryID = Data.cat_id;
		CategoryConstant.PrizeId = prizeID;
		LeaderBoard.serial = 1;
		Debug.Log(prizeID);
		SceneManager.LoadScene("TriviaLeaderboard");
		
	}
}
