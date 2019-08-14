using UnityEngine;
using System.Collections;

public class clearsettinginputforpass : MonoBehaviour {

	public dfTextbox textfield;
	
	public void OnClick()
	{
		textfield.Text = "";
		textfield.IsPasswordField = true;
	}
}
