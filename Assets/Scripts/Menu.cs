using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	[SerializeField] GameObject splash;

	void OnMouseDown () {
		if (!splash.activeSelf)
			MenuManager.manager.GoToGoal();
	}
}
