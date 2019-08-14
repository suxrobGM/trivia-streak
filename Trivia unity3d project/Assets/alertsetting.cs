using UnityEngine;
using System.Collections;

public class alertsetting : MonoBehaviour {

	public dfTextureSprite sound;
	public AudioClip clickBtn;
	
	public void OnClick()
	{
		GetComponent<AudioSource>().clip = clickBtn;
		GetComponent<AudioSource>().Play();
		if(GameConstant.IsSound == 1){
			sound.Texture = Resources.Load("music_icon_mute") as Texture2D;
			GameConstant.IsSound = 0;
		}else if(GameConstant.IsSound == 0){
			sound.Texture = Resources.Load("music_icon") as Texture2D;
			GameConstant.IsSound = 1;
		}
		
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));
		
		new HTTP.Request(TriviaService.GetHttpFolderPath()+"update_sound.php?user_id="+GameConstant.UserId+"&issound="+GameConstant.IsSound)
			.OnReply((reply) => {
				
			})
				.Send();
		//music.Texture = Resources.Load("alert_icon_mute") as Texture2D;
		
	}
}
