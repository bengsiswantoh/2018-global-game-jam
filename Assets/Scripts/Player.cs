using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float speed = 5;
	Vector3 goal;

	void Start () {
		goal = transform.position;
	}

	void Update () {
		if (transform.position != goal) {
			transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
		}
	}

	public void Move (Vector3 _goal) {
		goal = _goal;
	}
}
