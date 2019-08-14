using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class musicsetting : MonoBehaviour {
	public dfTextureSprite music;
	public AudioClip clickBtn;

	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();
		if(GameConstant.IsMusic == 1){
			music.Texture = Resources.Load("alert_icon_mute") as Texture2D;
			GameConstant.IsMusic = 0;
		}else if(GameConstant.IsMusic == 0){
			music.Texture = Resources.Load("alert_icon") as Texture2D;
			GameConstant.IsMusic = 1;
		}

		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"update_music.php?user_id="+GameConstant.UserId+"&ismusic="+GameConstant.IsMusic)
			.OnReply((reply) => {

			})
				.Send();
		//music.Texture = Resources.Load("alert_icon_mute") as Texture2D;

	}
}
