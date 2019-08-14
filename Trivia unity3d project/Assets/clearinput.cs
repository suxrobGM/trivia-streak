using UnityEngine;
using System.Collections;

public class clearinput : MonoBehaviour {

	public dfTextbox textfield;
	 
	public void OnClick()
	{
		textfield.Text = "";
	}
}
