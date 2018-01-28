using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGoal : MonoBehaviour {

	void OnMouseDown () {
		MenuManager.manager.Play();
	}
}
