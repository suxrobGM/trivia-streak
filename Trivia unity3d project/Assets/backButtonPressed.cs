using UnityEngine;
using UnityEngine.SceneManagement;

public class backButtonPressed : MonoBehaviour {

	public AudioClip clickBtn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				GetComponent<AudioSource>().clip = clickBtn;
				GetComponent<AudioSource>().Play();
				SceneManager.LoadScene ("TriviaLogin");
			}
		}
	}
}
