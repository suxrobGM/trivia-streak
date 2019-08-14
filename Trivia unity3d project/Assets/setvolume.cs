using UnityEngine;
using System.Collections;

public class setvolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GameConstant.IsMusic == 0){
			AudioListener.volume = 0.0F;
		}else{
			AudioListener.volume = 1.0F;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
