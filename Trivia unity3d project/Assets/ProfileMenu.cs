using UnityEngine;
using System.Collections;

public class ProfileMenu : MonoBehaviour
{

	public dfLabel username;
	public dfTextureSprite avatar;
	// Use this for initialization
	void Start ()
    {
		username.Text = GameConstant.UserId;
		StartCoroutine(DownloadImg());
	}

	public IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetImageDisplayUrl()+GameConstant.UserId+"_profile.jpg";
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);       
        yield return www;
		//if (www.error != null){
		avatar.Texture = www.texture as Texture;
	}
}
