using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kakera
{
public class PickerControllerNew : MonoBehaviour {

	[SerializeField]
	private Unimgpicker imagePicker;

	[SerializeField]
	private MeshRenderer imageRenderer;

	public dfTextureSprite icon;

	void Awake()
	{
		imagePicker.Completed += (string path) =>
		{
			StartCoroutine(LoadImage(path, imageRenderer));
		};
	}

	public void OnPressShowPicker()
	{
		imagePicker.Show("Select Image", "unimgpicker", 1024);
			Debug.Log("Pressed");
	}
			

	private IEnumerator LoadImage(string path, MeshRenderer output)
	{
		var url = "file://" + path;
		var www = new WWW(url);
		yield return www;

		var texture = www.texture;
		if (texture == null)
		{
			Debug.LogError("Failed to load texture url:" + url);
		}

		//output.material.mainTexture = texture;
			icon.Texture = texture;
	}
}
}
