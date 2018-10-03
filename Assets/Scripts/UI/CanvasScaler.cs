using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasScaler : MonoBehaviour {

	#region private fields

	private Canvas canvas;
	#endregion



	#region Unity Lifecycle

	public void Start()
	{

		canvas = GetComponent<Canvas>();
		
	}

	#endregion
}
