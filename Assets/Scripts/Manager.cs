using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public static GameObject player;
	public static int currentRoom;
	public static int goalRoom;

	private static Manager manager;
	private int _1Length;
	private int _2Length;
	private List<int> doorList;
	private static int [][] doors;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		player = GameObject.FindWithTag("Player");

		_1Length = 10;
		_2Length = 3;
		doorList = new List<int>();
		for (int i = 0; i < _1Length; i++) {
			for (int j = 0; j < _2Length; j++) {
				doorList.Add(i + 1);
			}
		}

		currentRoom = Random.Range(0, _1Length);
		currentRoom ++;
		print("current" + currentRoom);
		goalRoom = currentRoom;
		while (goalRoom == currentRoom) {
			goalRoom = Random.Range(0, _1Length);
			goalRoom ++;
		}

		generateDoors();
	}

	public static int getDoor(string door) {
		return doors[currentRoom][int.Parse(door)];
	}

	void generateDoors () {
		List<int> tempDoors = new List<int>(doorList);

		doors = new int [_1Length][];
		for (int i = 0; i < _1Length; i++) {
			doors[i] = new int[_2Length];
			for (int j = 0; j < _2Length; j++) {
				int index = Random.Range(0, tempDoors.Count);
				doors[i][j] = tempDoors[index];
				tempDoors.RemoveAt(index);
			}
		}
	}
}
