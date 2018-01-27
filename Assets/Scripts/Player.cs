using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float speed = 5;

	Vector3 goal;
	Vector3 startingPos;

	void Start () {
		startingPos = goal = transform.position;
	}

	void Update () {
		// Move player if position != goal
		if (transform.position != goal) {
			transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
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
	}

	public void Transmitte (int nextRoom) {
		RoomManager.manager.ChangeRoom(nextRoom);
		Reset();
	}
}
