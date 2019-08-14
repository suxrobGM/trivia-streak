using UnityEngine;
using System.Collections;

public class Catagory : MonoBehaviour
{
	public CatagoryData Data;

	public void OnMouseDown()
	{
		Debug.Log("Catagory is: " + Data.category_id);
	}
}
