using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour {

	[SerializeField] AudioClip [] musics;
	[SerializeField] Text doorPassedText;
	[SerializeField] Text currentRoomText;
	[SerializeField] Text goalRoomText;
	[SerializeField] Text timerText;

	public static RoomManager manager;
	[HideInInspector] public int currentRoom;
	[HideInInspector] public int goalRoom;
	[HideInInspector] public Player player;
	[HideInInspector] public int doorPassed;
	[HideInInspector] public int nextRoom;

	GameObject playerObject;
	int roomCount;
	int portalCount;
	List<int> doorList;
	int [][] doors;
	float timePassed;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		// set player
		playerObject = GameObject.FindWithTag("Player");
		if (playerObject != null)
			player = playerObject.GetComponent<Player>();

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

	void Update () {
		if (IsWin()) {
			WinGame();
		}

		UpdateTimer();
	}

	void UpdateInfo () {
		doorPassedText.text = "Portal yang dilewati : " + doorPassed;
		currentRoomText.text = "Current : " + currentRoom;

		for (int i = 0; i < 3; i ++) {
			int temp = GetDoor(i.ToString());
			print("door " + i + " : " + temp);
		}
	}

	void UpdateTimer () {
		if (currentRoom != goalRoom) {
			timePassed += Time.deltaTime;

			string minutes = Mathf.Floor(timePassed / 60).ToString("00");
 			string seconds = (timePassed % 60).ToString("00");

			timerText.text = "Waktu : " + minutes + ":" + seconds;
		}
	}

	void WinGame () {
		MenuManager.manager.ShowWinText(true);
	}

	public void InitGame () {
		timePassed = 0;
		doorPassed = 0;
		currentRoom = 0;

		MenuManager.manager.ShowWinText(false);

		// play bgm
		Game.manager.PlayMusic(musics[currentRoom]);

		// random goal room
		goalRoom = currentRoom;
		while (goalRoom == currentRoom) {
			goalRoom = Random.Range(0, roomCount);
		}

		RandomizeDoors();

		goalRoomText.text = "Goal : " + RoomManager.manager.goalRoom;
		UpdateInfo();
	}

	public void ReinitDoors () {
		RandomizeDoors();
		doorPassed ++;
		UpdateInfo();
		player.Reset();
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
		UpdateInfo();
	}

	public AudioClip GetCurrentMusic () {
		return musics[currentRoom];
	}
}
