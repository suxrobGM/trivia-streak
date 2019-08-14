using UnityEngine;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour {

	public dfTweenFloat Tween;

	// Use this for initialization
	void Start ()
    {
		Invoke("LoadNewLevel", 5);
	}

	void LoadNewLevel()
    {
		Tween.Play ();
		Tween.TweenCompleted += sender => 
        {
			SceneManager.LoadScene("TriviaSplash");
		};
	}
}
