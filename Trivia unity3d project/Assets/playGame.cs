using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour {
	public CountDown countObj;
	public dfTextureSprite mainPanel;
	public dfLabel questionLable;

	public dfTextureSprite answer1;
	public dfTextureSprite answer2;
	public dfTextureSprite answer3;
	public dfTextureSprite answer4;

	public dfLabel answer_text1;
	public dfLabel answer_text2;
	public dfLabel answer_text3;
	public dfLabel answer_text4;

	public dfTextureSprite countTexture1;
	public dfTextureSprite countTexture2;
	public dfTextureSprite countTexture3;
	public dfTextureSprite countTexture4;

	public dfLabel yourScoreLable;
	public dfLabel highScoreLable;

	public static QuestionReply catReply;
	public static int questionCounter = 0;
	public static int totalQuestion;

	public dfLabel question_no;

	public static bool isPaused;
  	
	public void Start(){

		if (CategoryConstant.GameType.Equals("fun"))
		{
			if (GameConstant.CurrentScore > GameConstant.FunHighScore)
			{
				//highScoreLable.Text = gameConsatant.currentScore.ToString();
				highScoreLable.Text = CategoryConstant.PrizeTopScore.ToString();
			}
            else {
				//highScoreLable.Text = gameConsatant.FunHighScore.ToString();
				highScoreLable.Text = CategoryConstant.PrizeTopScore.ToString();
			}

		}
		//Debug.Log("Score = "+gameConsatant.currentScore.ToString());

		//countObj.start ();
		if (catReply == null)
		{
		    HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
			new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_questions.php?category_id="+CategoryConstant.MainCategoryId+"&parent_id="+CategoryConstant.SubCategoryId+"&grand_parant_id="+CategoryConstant.GrandCategoryId)
			.OnReply(reply => 
            {	
				catReply = JsonConvert.DeserializeObject<QuestionReply>(reply.DataAsString);

				if (catReply.success == 1)
                {
					totalQuestion = catReply.data.Count;
					StartRound();
				}				
			
			})
            .Send();
		}
        else {
			StartRound();
		}


		//mainPanel.IsVisible = false;
		//StartCoroutine(Wait(2.0f));
		//Invoke( "LoadNewLevel", 5 );
	}

	public void StartRound()
	{
		Color32 myNewColor = new Color32(196,115,7,255); //RBGA in bytes
		//answer1.BackgroundColor = myNewColor;
		//answer2.BackgroundColor = myNewColor;
		//answer3.BackgroundColor = myNewColor;
		//answer4.BackgroundColor = myNewColor;

		if (questionCounter < totalQuestion)
		{
			int showNumber = questionCounter+1;
			question_no.Text = "Question "+showNumber+" of 10";

			answer1.IsEnabled = true;
			answer2.IsEnabled = true;
			answer3.IsEnabled = true;
			answer4.IsEnabled = true;

			answer1.Texture = Resources.Load("checked_field_bg") as Texture2D;
			answer2.Texture = Resources.Load("checked_field_bg") as Texture2D;
			answer3.Texture = Resources.Load("checked_field_bg") as Texture2D;
			answer4.Texture = Resources.Load("checked_field_bg") as Texture2D;


			//string aaa = catReply.data[questionCounter].qu_text;//text to decode.
			//string b = aaa.Replace("&#039;", "'");
			//questionLable.Text = b.Replace("&quot;", "\"");

			questionLable.Text = DecodeString(catReply.data[questionCounter].qu_text);

			countTexture1.Texture = Resources.Load("yellow_num_bg") as Texture2D;
			countTexture2.Texture  = Resources.Load("yellow_num_bg") as Texture2D;
			countTexture3.Texture  = Resources.Load("yellow_num_bg") as Texture2D;
			countTexture4.Texture  = Resources.Load("yellow_num_bg") as Texture2D;

			countTexture1.GetComponentInChildren<dfLabel>().OutlineColor = myNewColor;
			countTexture2.GetComponentInChildren<dfLabel>().OutlineColor = myNewColor;
			countTexture3.GetComponentInChildren<dfLabel>().OutlineColor = myNewColor;
			countTexture4.GetComponentInChildren<dfLabel>().OutlineColor = myNewColor;

			countTexture1.GetComponentInChildren<dfLabel>().IsVisible = true;
			countTexture2.GetComponentInChildren<dfLabel>().IsVisible = true;
			countTexture3.GetComponentInChildren<dfLabel>().IsVisible = true;
			countTexture4.GetComponentInChildren<dfLabel>().IsVisible = true;

			answer_text1.Text = DecodeString(catReply.data[questionCounter].qu_option1);
			answer_text2.Text = DecodeString(catReply.data[questionCounter].qu_option2);
			answer_text3.Text = DecodeString(catReply.data[questionCounter].qu_option3);
			answer_text4.Text = DecodeString(catReply.data[questionCounter].qu_option4);

			//  check if answer 1
			if(catReply.data[questionCounter].qu_answer == 1){
				answer1.GetComponent<answerQuestion>().isCorrect = true;
			}else{
				answer1.GetComponent<answerQuestion>().isCorrect = false;
			}
			//  check if answer 2
			if(catReply.data[questionCounter].qu_answer == 2){
				answer2.GetComponent<answerQuestion>().isCorrect = true;
			}else{
				answer2.GetComponent<answerQuestion>().isCorrect = false;
			}
			//  check if answer 3
			if(catReply.data[questionCounter].qu_answer == 3){
				answer3.GetComponent<answerQuestion>().isCorrect = true;
			}else{
				answer3.GetComponent<answerQuestion>().isCorrect = false;
			}
			//  check if answer 4
			if(catReply.data[questionCounter].qu_answer == 4){
				answer4.GetComponent<answerQuestion>().isCorrect = true;
			}else{
				answer4.GetComponent<answerQuestion>().isCorrect = false;
			}
			//  end

			answer1.GetComponent<answerQuestion>().questionDifficulty = catReply.data[questionCounter].qu_difficulty;
			answer2.GetComponent<answerQuestion>().questionDifficulty = catReply.data[questionCounter].qu_difficulty;
			answer3.GetComponent<answerQuestion>().questionDifficulty = catReply.data[questionCounter].qu_difficulty;
			answer4.GetComponent<answerQuestion>().questionDifficulty = catReply.data[questionCounter].qu_difficulty;

			//Debug.Log(catReply.data[questionCounter].qu_text);
			//Debug.Log(catReply.data[questionCounter].qu_answer);

			countObj.start ();
		}
		else
		{
			Debug.Log("Game Over");
			catReply = null;
			questionCounter = 0;
			totalQuestion = 0;
			SceneManager.LoadScene ("TriviaResult");
		}
	}
	private IEnumerator Wait(float seconds)
	{
		Debug.Log("waiting");
		yield return new WaitForSeconds(seconds);
		mainPanel.IsVisible = true;
		Debug.Log("wait end");
	}

	void Update()
	{
		if(countObj.Update() == 0){
			questionCounter++;
			Start();
		}


		//	questionLable.Text = "new question" + counter--;

		// This is if statement checks how many seconds there are to decide what to do.
		// If there are more than 0 seconds it will continue to countdown.
		// If not then it will reset the amount of seconds to 59 and handle the minutes;
		// Handling the minutes is very similar to handling the seconds.
		/*if(Seconds <= 0)
		{
			Seconds = 9;
			if(Minutes >= 1)
			{
				Minutes--;
			}
			else
			{
				Minutes = 0;
				Seconds = 0;
				// This makes the guiText show the time as X:XX. ToString.("f0") formats it so there is no decimal place.
			}
		}
		else
		{
			Seconds -= Time.deltaTime;
		}
		leftDigit.Text = "0";
		rightDigit.Text = Seconds.ToString("f0");
		Debug.Log(Seconds.ToString("f0"));
		*/
	
	}


	void OnApplicationPause(bool pauseStatus)
	{
		isPaused = true;
	}

	void OnApplicationFocus(bool pauseStatus)
	{
		if(isPaused)
		{
			catReply = null;
			questionCounter = 0;
			totalQuestion = 0;
			SceneManager.LoadScene ("TriviaResult");
		}else{
			catReply = null;
			questionCounter = 0;
			totalQuestion = 0;
			SceneManager.LoadScene ("TriviaResult");
		}
	}
	public string DecodeString(string str){
		string aaa = str;
		string b = aaa.Replace("&#039;", "'");
		string c = b.Replace("&quot;", " ");
		string d = c.Replace("&amp;", "&");

		return d;
	}

}