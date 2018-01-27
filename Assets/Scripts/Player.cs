using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField] float speed = 5;
	[SerializeField] Text doorPassed;
	[SerializeField] Text currentRoom;
	[SerializeField] Text goalRoom;
	[SerializeField] Text timer;

	Vector3 goal;
	Vector3 startingPos;

	void Start () {
		startingPos = goal = transform.position;
		UpdateDoorPassed();
		UpdateCurrentRoom();
		goalRoom.text = "Goal : " + Manager.goalRoom;
		printInfo();
	}

	void Update () {
		if (transform.position != goal) {
			transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Portal") {
			Reset();
			Manager.currentRoom = Manager.GetDoor(other.name);
			Manager.doorPassed ++;
			UpdateDoorPassed();
			UpdateCurrentRoom();
			printInfo();
		}
	}

	void UpdateDoorPassed () {
		doorPassed.text = "Portal passed : " + Manager.doorPassed;
	}

	void UpdateCurrentRoom () {
		currentRoom.text = "Current : " + Manager.currentRoom;
	}

	public void Move (Vector3 _goal) {
		goal = _goal;
	}

	public bool IsMoving () {
		return transform.position != goal;
	}

	public void Reset () {
		transform.position = goal = startingPos;
	}

	public void printInfo() {
		for (int i = 0; i < 3; i ++) {
			int temp = Manager.GetDoor(i.ToString());
			print("door " + i + " : " + temp);
		}
	}
}
