using UnityEngine;
using UnityEngine.SceneManagement;

public class goToProfile : MonoBehaviour {
	public dfTweenFloat Tween;
	public void OnClick()
	{
		Tween.Play ();
		Tween.TweenCompleted += (dfTweenPlayableBase sender) => {
		    SceneManager.LoadScene ("profile");
		};
	}
}
