using UnityEngine;
using System.Collections;

public class hideNavigationDrawer : MonoBehaviour {

	public dfTweenVector3 Tween;
	// Use this for initialization
	public void OnClick()
	{
		Tween.StartValue = new Vector3(0, 0, 0);
		Tween.EndValue = new Vector3(-600, 0, 0);
		Tween.Play ();
		Tween.TweenCompleted += (dfTweenPlayableBase sender) => {
			
		};
		
	}
}
