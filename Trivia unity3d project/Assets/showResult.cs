using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class showResult : MonoBehaviour {

	public dfLabel correctanswer;
	public dfLabel difficultyanswer;
	public dfLabel speedanswer;
	public dfLabel pointscore;

	public dfLabel totalpointscore;
	public dfLabel userplace;
	public dfLabel dailygame;

	public AudioClip resultSound;
	public int soundCounter = 1;
	// Use this for initialization
	void Start () {
		correctanswer.Text = "Correct Answers:       "+GameConstant.CorrectAnswerCount;
		difficultyanswer.Text = "Difficulty Bonus:       "+GameConstant.DifficultyAnswerCount;
		speedanswer.Text = "Speed Bonus:       "+GameConstant.SpeedAnswerCount;
		pointscore.Text = "Points Earned:       "+GameConstant.CurrentScore;

		//  insert score 

		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));

		new HTTP.Request(TriviaService.GetHttpFolderPath()+"add_game_score.php?game_user_id="+GameConstant.UserId+"&game_user_type="+GameConstant.UserType+"&game_points="+GameConstant.CurrentScore+"&cat_id="+CategoryConstant.GrandCategoryId+"&sec_cat_id="+CategoryConstant.SubCategoryId+"&th_cat_id="+CategoryConstant.MainCategoryId+"&prize_id="+CategoryConstant.PrizeId)
			.OnReply((reply) => {
				
				AddScoreReply scoreReply = JsonConvert.DeserializeObject<AddScoreReply>(reply.DataAsString);
				if(scoreReply.success == 1){
					//  Grab user current game Rank 
					
					new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_player_game_rank.php?game_user_id="+GameConstant.UserId+"&game_user_type="+GameConstant.UserType+"&cat_id="+CategoryConstant.GrandCategoryId+"&sec_cat_id="+CategoryConstant.SubCategoryId+"&th_cat_id="+CategoryConstant.MainCategoryId+"&prize_id="+CategoryConstant.PrizeId)
						.OnReply((reply1) => {
							
							RankReply rankReply = JsonConvert.DeserializeObject<RankReply>(reply1.DataAsString);
							totalpointscore.Text = rankReply.points.ToString();
							userplace.Text = rankReply.rank.ToString()+"th Place";
							
						}).Send();

					//  Grab user current daily game
					
					new HTTP.Request(TriviaService.GetHttpFolderPath()+"get_player_game.php?game_user_id="+GameConstant.UserId+"&game_user_type="+GameConstant.UserType)
						.OnReply((reply2) => {
							
							DailyGameReply dailyReply = JsonConvert.DeserializeObject<DailyGameReply>(reply2.DataAsString);
							dailygame.Text = dailyReply.daily_game.ToString();							
						}).Send();
				}

			}).Send();

		GameConstant.CorrectAnswerCount = 0;
		GameConstant.DifficultyAnswerCount = 0;
		GameConstant.SpeedAnswerCount = 0;
		GameConstant.CurrentScore = 0;
	
	}
	void Update(){
		GetComponent<AudioSource>().clip = resultSound;
		if(soundCounter <= 2){
			if(!GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Play();
				soundCounter++;
			}
		}
	}
}