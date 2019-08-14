using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class test : MonoBehaviour {

	public Catagory Prefab;

	void Start ()
	{
		HTTP.Client.Instance.Configure(new HTTP.Settings(TriviaService.GetHostAddress()).Protocol(HTTP.Protocol.HTTP));

		new HTTP.Request(TriviaService.GetHttpFolderPath()+"all_categories.php?restaurant_id=5")
		.OnReply((reply) => {
				/*
				JObject obj = JObject.Parse(reply.DataAsString);
				JArray list = obj["data"] as JArray;
				foreach (JToken token in list)
				{
					CatagoryData data =	JsonConvert.DeserializeObject<CatagoryData>(token.ToString());
					Catagory instance = Instantiate(Prefab) as Catagory;
					instance.Data = data;
				}
				*/
				CatagoryReply catReply = JsonConvert.DeserializeObject<CatagoryReply>(reply.DataAsString);
				foreach(CatagoryData data in catReply.data)
				{
					Catagory instance = Instantiate(Prefab) as Catagory;
					instance.Data = data;
				}
			})
		.Send();
	}

}
