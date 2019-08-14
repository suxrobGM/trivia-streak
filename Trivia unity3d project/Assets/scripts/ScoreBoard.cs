using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

	public dfLabel lable_score;
	// Use this for initialization
	void Start () {
		lable_score.Text = GameConstant.CurrentScore.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
