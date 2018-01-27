using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	[SerializeField] AudioClip [] musics;

	public static RoomManager manager;
	[HideInInspector] public int currentRoom;
	[HideInInspector] public int goalRoom;
	[HideInInspector] public Player playerScript;
	[HideInInspector] public int doorPassed;
	[HideInInspector] public int nextRoom;

	GameObject player;
	int roomCount;
	int portalCount;
	List<int> doorList;
	int [][] doors;

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
		roomCount = musics.Length;
		portalCount = 3;
		doorList = new List<int>();
		for (int i = 0; i < roomCount; i ++) {
			for (int j = 0; j < portalCount; j ++) {
				doorList.Add(i);
			}
		}
	}

	void Start () {
		InitGame();
	}

	void Update () {
		if (IsWin()) {
			WinGame();
		}
	}

	void WinGame () {
		MenuManager.manager.ShowWinMenu(true);
	}

	public void InitGame () {
		Game.manager.StopGame(false);

		// random starting room
		// currentRoom = Random.Range(0, roomCount);
		currentRoom = 0;

		// play bgm
		Game.manager.PlayMusic(musics[currentRoom]);

		// random goal room
		goalRoom = currentRoom;
		while (goalRoom == currentRoom) {
			goalRoom = Random.Range(0, roomCount);
		}

		doorPassed = 0;
		RandomizeDoors();
	}

	public void ReinitGame () {
		RandomizeDoors();
		doorPassed ++;
		playerScript.UpdateDoorPassed();
		playerScript.Reset();
		playerScript.printInfo();
	}

	public void RandomizeDoors () {
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

	public bool IsWin() {
		return currentRoom == goalRoom;
	}

	public int GetDoor (string door) {
		return doors[currentRoom][int.Parse(door)];
	}

	public void ChangeRoom (int nextRoom) {
		currentRoom = nextRoom;
		doorPassed ++;
		Game.manager.PlayMusic(musics[currentRoom]);
	}

	public AudioClip GetCurrentMusic () {
		return musics[currentRoom];
	}
}
