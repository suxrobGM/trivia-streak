using UnityEngine;
using System.Collections;

public class checkGameType : MonoBehaviour {


	// Use this for initialization
	void Start () {
		if(CategoryConstant.GameType.Equals("fun"))	{
			Destroy(gameObject);
		}
	}

}
