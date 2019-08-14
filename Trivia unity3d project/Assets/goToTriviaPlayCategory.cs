using UnityEngine;
using UnityEngine.SceneManagement;

public class goToTriviaPlayCategory : MonoBehaviour {

	public int prizeID;
	public int prizeTopScore;

	public void OnMouseDown()
	{
		//categoryConstant.MainCategoryID = Data.cat_id;
		CategoryConstant.PrizeId = prizeID;
		CategoryConstant.PrizeTopScore = prizeTopScore;
		Debug.Log(prizeID);
		SceneManager.LoadScene("TriviaCategory");
		
	}
}
