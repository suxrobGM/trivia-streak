using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLogin : MonoBehaviour {

	public void OnClick()
	{
		SceneManager.LoadScene ("login");
	}
}
