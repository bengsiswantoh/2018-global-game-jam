using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winText;

	public static GameObject player;
	public static int currentRoom;
	public static int goalRoom;
	public static int doorPassed;

	static Manager manager;
	static Player playerScript;
	static int [][] doors;

	int roomCount;
	int portalCount;
	List<int> doorList;
	bool paused;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		// set player
		player = GameObject.FindWithTag("Player");
		if (player != null)
			playerScript = player.GetComponent<Player>();

		// make doorlist to generate better random
		roomCount = 10;
		portalCount = 3;
		doorList = new List<int>();
		for (int i = 0; i < roomCount; i ++) {
			for (int j = 0; j < portalCount; j ++) {
				doorList.Add(i);
			}
		}

		InitGame();
	}

	void Update () {
		if (Input.GetButtonDown("Cancel"))
			TogglePauseMenu();

		if (currentRoom == goalRoom) {
			WinGame();
		}
	}

	void RandomizeDoors () {
		List<int> tempDoors = new List<int>(doorList);

		doors = new int [roomCount][];
		for (int i = 0; i < roomCount; i ++) {
			doors[i] = new int[portalCount];
			for (int j = 0; j < portalCount; j ++) {
				int index = Random.Range(0, tempDoors.Count);
				doors[i][j] = tempDoors[index];
				tempDoors.RemoveAt(index);
			}
		}
	}

	void InitGame () {
		StopGame(false);

		// random starting room
		currentRoom = Random.Range(0, roomCount);

		// random goal room
		goalRoom = currentRoom;
		while (goalRoom == currentRoom) {
			goalRoom = Random.Range(0, roomCount);
		}

		doorPassed = 0;
		RandomizeDoors();
	}

	void StopGame (bool stop) {
		paused = stop;
		Time.timeScale = paused ? 0 : 1;
	}

	public void TogglePauseMenu () {
		StopGame(!paused);
		if (pauseMenu != null)
			pauseMenu.SetActive(paused);
	}

	public void ResetGameButton () {
		TogglePauseMenu();
		playerScript.Reset();
		InitGame();
	}

	void WinGame () {
		StopGame(true);
		if (winText != null)
			winText.SetActive(paused);
	}

	public static Player GetPlayer () {
		return playerScript;
	}

	public static int GetDoor (string door) {
		return doors[currentRoom][int.Parse(door)];
	}
}
