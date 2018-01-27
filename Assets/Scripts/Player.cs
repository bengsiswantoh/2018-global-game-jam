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
	float timePassed;

	void Start () {
		startingPos = goal = transform.position;
		UpdateDoorPassed();
		UpdateCurrentRoom();
		goalRoom.text = "Goal : " + Manager.goalRoom;
		printInfo();
	}

	void Update () {
		// Move player if position != goal
		if (transform.position != goal) {
			transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
		}

		// Update time
		if (Manager.currentRoom != Manager.goalRoom) {
			timePassed += Time.deltaTime;

			string minutes = Mathf.Floor(timePassed / 60).ToString("00");
 			string seconds = (timePassed % 60).ToString("00");

			timer.text = "Waktu : " + minutes + ":" + seconds;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Portal") {
			int nextRoom = Manager.GetDoor(other.name);
			// nextRoom = Manager.goalRoom;

			if (nextRoom == Manager.goalRoom) {
				Manager.ShowQuiz(nextRoom);
			} else
				Transmitte(nextRoom);
		}
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

	public void Transmitte (int nextRoom) {
		Manager.currentRoom = nextRoom;
		Manager.doorPassed ++;
		UpdateDoorPassed();
		UpdateCurrentRoom();
		Reset();
		printInfo();
	}

	public void UpdateDoorPassed () {
		doorPassed.text = "Portal yang dilewati : " + Manager.doorPassed;
	}

	public void printInfo() {
		for (int i = 0; i < 3; i ++) {
			int temp = Manager.GetDoor(i.ToString());
			print("door " + i + " : " + temp);
		}
	}
}
