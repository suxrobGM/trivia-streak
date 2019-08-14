using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	public static int seconds = 12;
	public dfTextureSprite timer;

	public dfLabel countRightLable;
	//public dfLabel countLeftLable;


	// Use this for initialization
	public void start () {
		seconds = 12;
		//countLeftLable.Text = "1";
		InvokeRepeating("Countdown",1.0f,1.0f);
	}
	
	public void Countdown(){
		if(-- seconds == 0){
			CancelInvoke("Countdown");
		}
		countRightLable.Text = seconds.ToString();
		//countLeftLable.Text = "0";
	}
	public int Update(){
		AudioSource timerClip =  timer.GetComponent<AudioSource>();
		if(seconds <= 0){
			//Application.LoadLevel("Defeat");
			if(timerClip.isPlaying)
			{
				timerClip.Stop();
			}
		}
		if(seconds <= 5 && seconds > 0){

			if(!timerClip.isPlaying)
			{
				timerClip.Play();
			}
		}
		return seconds;
	}

	public void stop(){
		CancelInvoke("Countdown");
		countRightLable.Text = "0";
		//countLeftLable.Text = "0";
	}

}