using UnityEngine;
using System.Collections;

public class stopaudio : MonoBehaviour {

	public dfTextureSprite mainpanel;
	public AudioClip clickBtn;
	public dfSpriteAnimation progress;
	public void OnClick()
	{
		AudioSource backgroundClip =  mainpanel.GetComponent<AudioSource>();
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();
		if(backgroundClip.isPlaying)
		{
			backgroundClip.Stop();
			progress.Pause();
		}
	
	}
}
