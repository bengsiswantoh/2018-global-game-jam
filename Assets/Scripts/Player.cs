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
		goalRoom.text = "Goal : " + RoomManager.manager.goalRoom;
		printInfo();
	}

	void Update () {
		// Move player if position != goal
		if (transform.position != goal) {
			transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
		}

		// Update time
		if (RoomManager.manager.currentRoom != RoomManager.manager.goalRoom) {
			timePassed += Time.deltaTime;

			string minutes = Mathf.Floor(timePassed / 60).ToString("00");
 			string seconds = (timePassed % 60).ToString("00");

			timer.text = "Waktu : " + minutes + ":" + seconds;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Portal") {
			int nextRoom = RoomManager.manager.GetDoor(other.name);
			nextRoom = RoomManager.manager.goalRoom;

			if (nextRoom == RoomManager.manager.goalRoom) {
				QuizManager.manager.ShowQuiz(nextRoom);
			} else
				Transmitte(nextRoom);
		}
	}

	public void Move (Vector3 _goal) {
		goal = _goal;
	}

	public bool IsMoving () {
		return transform.position != goal;
	}

	public void Reset () {
		transform.position = goal = startingPos;
		UpdateCurrentRoom();
	}

	public void Transmitte (int nextRoom) {
		RoomManager.manager.ChangeRoom(nextRoom);
		UpdateDoorPassed();
		UpdateCurrentRoom();
		Reset();
		printInfo();
	}

	public void UpdateCurrentRoom () {
		currentRoom.text = "Current : " + RoomManager.manager.currentRoom;
	}

	public void UpdateDoorPassed () {
		doorPassed.text = "Portal yang dilewati : " + RoomManager.manager.doorPassed;
	}

	public void printInfo() {
		for (int i = 0; i < 3; i ++) {
			int temp = RoomManager.manager.GetDoor(i.ToString());
			print("door " + i + " : " + temp);
		}
	}
}
