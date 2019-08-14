using UnityEngine;
using System.Collections;

public class showNavigationDrawerLeft : MonoBehaviour {

	public dfTweenVector3 Tween;
	// Use this for initialization
	public void OnClick()
	{
		//Tween.StartValue = new Vector3(720, 0, 0);
		//Tween.EndValue = new Vector3(220, 0, 0);

		//Tween.StartValue = new Vector3(720, 0, 0);
		Tween.EndValue = new Vector3(-500, 0, 0);
		Tween.Play ();
		Tween.TweenCompleted += (dfTweenPlayableBase sender) => {
			
		};
		
	}
}
