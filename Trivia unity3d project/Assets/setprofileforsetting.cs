using UnityEngine;
using System.Collections;

public class setprofileforsetting : MonoBehaviour {

	public dfLabel username;
	public dfTextbox useremail;
	public dfTextureSprite avatar;
	public dfDropdown country;

	public dfTextureSprite music;
	public dfTextureSprite sound;
	// Use this for initialization
	void Start () {
		username.Text = GameConstant.UserId;
		useremail.Text = GameConstant.UserEmail;
		if(GameConstant.UserCountry == null){
			country.SelectedValue = "Select Country";
		}else{
			country.SelectedValue = GameConstant.UserCountry.ToString();
		}
		if(GameConstant.IsMusic == 1){
			music.Texture = Resources.Load("alert_icon") as Texture2D;
		}else if(GameConstant.IsMusic == 0){
			music.Texture = Resources.Load("alert_icon_mute") as Texture2D;
		}

		if(GameConstant.IsSound == 1){
			sound.Texture = Resources.Load("music_icon") as Texture2D;
		}else if(GameConstant.IsSound == 0){
			sound.Texture = Resources.Load("music_icon_mute") as Texture2D;
		}

		StartCoroutine(DownloadImg());
	}
	IEnumerator DownloadImg ()
	{
		string url = TriviaService.GetImageDisplayUrl()+GameConstant.UserId+"_profile.jpg";
		//Texture2D texture = new Texture2D(1,1);
		WWW www = new WWW(url);
		yield return www;
		//if (www.error != null){
		avatar.Texture = www.texture as Texture;
	}
}
