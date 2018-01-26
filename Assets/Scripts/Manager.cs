using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public static GameObject player;

	private static Manager manager;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		player = GameObject.FindWithTag("Player");
		// DontDestroyOnLoad(gameObject);
	}
}
