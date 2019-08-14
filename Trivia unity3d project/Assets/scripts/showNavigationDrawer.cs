using UnityEngine;
using System.Collections;

public class showNavigationDrawer : MonoBehaviour {

	public dfTweenVector3 Tween;
	public AudioClip clickBtn;

	// Use this for initialization
	public void OnClick()
	{
		if (GameConstant.IsMusic == 1)
        {
		    GetComponent<AudioSource>().clip = clickBtn;
		    GetComponent<AudioSource>().Play();
		}

		Tween.StartValue = new Vector3(-600, 0, 0);
		Tween.EndValue = new Vector3(0, 0, 0);
		Tween.Play ();

		Tween.TweenCompleted += sender => 
        {
		};		
	}
}
