using UnityEngine;
using UnityEngine.SceneManagement;

public class goToRegister : MonoBehaviour {
	public dfTweenFloat Tween;
	public void OnClick()
	{
		Tween.Play ();
		Tween.TweenCompleted += (dfTweenPlayableBase sender) => {
			SceneManager.LoadScene ("register");
		};
	}
}
