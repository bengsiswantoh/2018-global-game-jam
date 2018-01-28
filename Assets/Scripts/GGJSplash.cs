using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGJSplash : MonoBehaviour {

	void Start () {
		StartCoroutine("HideSplash");
	}

	IEnumerator HideSplash () {
		yield return new WaitForSeconds(1);
		gameObject.SetActive(false);
	}
}
