using UnityEngine;
using System.Collections;

public class searchSubCategory : MonoBehaviour {

	public dfTextbox seachText;

	public void OnMouseDown()
	{
		Debug.Log(seachText.Text);
	}
}
