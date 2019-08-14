using UnityEngine;
using UnityEngine.SceneManagement;

public class waitFor5Second : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		Invoke( "LoadNewLevel", 5 );	
	}
	void LoadNewLevel()
    {
		SceneManager.LoadScene ("TriviaResult");
	}
}
