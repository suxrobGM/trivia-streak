using UnityEngine;
using System.Collections;

public class answerQuestion : MonoBehaviour {
	public int questionOption;
	public bool isCorrect = false;
	public string questionDifficulty;
	public dfTextureSprite buttonPanel;

	public CountDown countObj;
	public playGame playObj;

	public dfLabel yourScoreLable;

	public dfTextureSprite countTexture;

	public dfTextureSprite other1;
	public dfTextureSprite other2;
	public dfTextureSprite other3;

	public dfTextureSprite other_icon1;
	public dfTextureSprite other_icon2;
	public dfTextureSprite other_icon3;

	//   sounds clip
	public AudioClip rightAnswerClip;
	public AudioClip wrongAnswerClip;

	public dfTextureSprite timer;
	public void OnClick()
	{
		this.gameObject.AddComponent<AudioSource>();

		if(countObj.Update() > 0){
			countObj.stop();

			AudioSource timerClip =  timer.GetComponent<AudioSource>();
			if(timerClip.isPlaying)
			{
				timerClip.Stop();
			}
			if(isCorrect)
			{
				//Color32 myNewColor = new Color32(0,126,40,255); //RBGA in bytes
				//buttonPanel.BackgroundColor = myNewColor;
				GetComponent<AudioSource>().clip = rightAnswerClip;
				GetComponent<AudioSource>().Play();
				//audio.PlayOneShot(rightAnswerClip, 0.7F);
				countTexture.Texture = Resources.Load("check") as Texture2D;
				countTexture.GetComponentInChildren<dfLabel>().IsVisible = false;

				GameConstant.CurrentScore += GameConstant.CorrectAnswer;
				GameConstant.CorrectAnswerCount += GameConstant.CorrectAnswer;
				if(CountDown.seconds >= 7)
				{
					GameConstant.CurrentScore += GameConstant.CorrectAnswerInSevenToTenSec;
					GameConstant.SpeedAnswerCount += GameConstant.CorrectAnswerInSevenToTenSec;
				}else if(CountDown.seconds >= 4)
				{
					GameConstant.CurrentScore += GameConstant.CorrectAnswerInSixToFourSec;
					GameConstant.SpeedAnswerCount += GameConstant.CorrectAnswerInSixToFourSec;
				}else if(CountDown.seconds < 4)
				{
					GameConstant.CurrentScore += GameConstant.CorrectAnswerInThreeToZeroSec;
					GameConstant.SpeedAnswerCount += GameConstant.CorrectAnswerInThreeToZeroSec;
				}


				if(questionDifficulty.Equals("EASY"))
				{
					GameConstant.CurrentScore += GameConstant.CorrectEasyAnswer;
					GameConstant.DifficultyAnswerCount += GameConstant.CorrectEasyAnswer;
				}else if(questionDifficulty.Equals("MEDIUM"))
				{
					GameConstant.CurrentScore += GameConstant.CorrectMediumAnswer;
					GameConstant.DifficultyAnswerCount += GameConstant.CorrectMediumAnswer;
				}else if(questionDifficulty.Equals("DIFFICULT"))
				{
					GameConstant.CurrentScore += GameConstant.CorrectHardAnswer;
					GameConstant.DifficultyAnswerCount += GameConstant.CorrectHardAnswer;
				}

			}
			else{
				//Color32 myNewColor = new Color32(255,0,0,255); //RBGA in bytes
				//buttonPanel.BackgroundColor = myNewColor;
				//audio.PlayOneShot(wrongAnswerClip, 0.7F);
				GetComponent<AudioSource>().clip = wrongAnswerClip;
				GetComponent<AudioSource>().Play();

				countTexture.Texture = Resources.Load("cross") as Texture2D;
				countTexture.GetComponentInChildren<dfLabel>().IsVisible = false;
			}

			Color32 myNewColor = new Color32(118,118,118,255); //RBGA in bytes

			other1.IsEnabled = false;
			other1.Texture = Resources.Load("gray_field_bg") as Texture2D;
			other_icon1.Texture = Resources.Load("gray_num_bg") as Texture2D;
			other_icon1.GetComponentInChildren<dfLabel>().OutlineColor  = myNewColor;

			other2.IsEnabled = false;
			other2.Texture = Resources.Load("gray_field_bg") as Texture2D;
			other_icon2.Texture = Resources.Load("gray_num_bg") as Texture2D;
			other_icon2.GetComponentInChildren<dfLabel>().OutlineColor  = myNewColor;

			other3.IsEnabled = false;
			other3.Texture = Resources.Load("gray_field_bg") as Texture2D;
			other_icon3.Texture = Resources.Load("gray_num_bg") as Texture2D;
			other_icon3.GetComponentInChildren<dfLabel>().OutlineColor  = myNewColor;

			StartCoroutine(Wait(4.0f));
		}
		//Color32 myNewColor = new Color32(128,128,128,255); //RBGA in bytes
		//buttonPanel.BackgroundColor = myNewColor;

		/*float cur_sec = playGame.Seconds;
	
		if(cur_sec >= 0){
			playGame.Seconds = 0;
			Debug.Log(questionOption);
			playGame.Seconds = 9;
		}*/
	}
	private IEnumerator Wait(float seconds)
	{
		yourScoreLable.Text = GameConstant.CurrentScore.ToString();
		//Debug.Log("waiting");
		yield return new WaitForSeconds(seconds);
		//Debug.Log("wait end");
		playGame.questionCounter++;
		playObj.Start();
	}
}
