using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown () {
		Player player;
		if (Manager.player != null) {
			player = Manager.player.GetComponent<Player>();
			if (player != null)
				player.Move(transform.position);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			print("trigger:" + Manager.getDoor(gameObject.name));
		}
	}
}
