using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winMenu;
	[SerializeField] GameObject quizMenu;
	[SerializeField] Text quizQuestion;
	[SerializeField] InputField quizAnswer;
	[SerializeField] Text quizResult;
	[SerializeField] AudioClip [] musics;

	public static Manager manager;
	public static int currentRoom;
	public static int goalRoom;
	public static int doorPassed;

	GameObject player;
	Player playerScript;
	int [][] doors;
	int nextRoom;
	int quizIndex;
	string [] questions = new string [] {
		"Kita bisa melihat wajah kita di '_ _ _ A'",
		"Nama kota di provinsi Jawa Barat 'B _ _ _ _ _'",
		"Orang mati biasanya naik '_ _ _ _ _ _ A'",
		"Bisa SMS kalau ada '_ _ L _ _'",
		"Dunia tak selebar daun '_ E _ _ _'",
		"Lari jarak jauh 'M _ _ _ _ _ O _'",
		"Olahraga membuat badan 'S _ _ _ _'",
		"Valentiona Rossi berasal dari 'I _ _ _ _ _",
		"Yang membatalkan puasa '_ _ _ _ N'",
		"Kata untuk menyapa orang di medan 'H _ _ _ _'",
		"Koran disebut surat '_ _ _ A _'",
		"Nama binatang yamh diulang '_ _ R _ _ _'",
		"Pasang bendera di 'T _ _ _ _'",
		"Puasa dibulan '_ _ _ _ _ H _ N'",
		"Terompet jika ditiup akan 'B _ _ _ _'",
		"Monumen di Jakarta itu '_ _ _ A _'",
		"Kebanyakan orang tidur waktu '_ _ _ _ M'",
		"Dari Bandung ke Surabaya kita menggunakan '_ E _ _ _ A'",
		"Dapat bergerak sendiri tanpa disuruh '_ O _ _ _'",
		"Jika anda dikelilingi oleh 300 Harimau, apa yang akan anda lakukan '_ A _ _ A _'"
	};
	string [] answers = new string [] {
		"sima",
		"banyak",
		"apasaja",
		"hplah",
		"betul",
		"malesloh",
		"salah",
		"ibunya",
		"adzan",
		"hallo",
		"salah",
		"jarang",
		"tarik",
		"kejauhan",
		"basah",
		"keras",
		"merem",
		"celana",
		"roboh",
		"santai"
	};

	int roomCount;
	int portalCount;
	List<int> doorList;
	bool paused;
	AudioSource stingSource;

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

		// play bgm
		if (Game.musicOn) {
			// stingSource.clip = musics[currentRoom];
			// stingSource.Play();
		}

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

	void WinGame () {
		StopGame(true);
		if (winMenu != null)
			winMenu.SetActive(paused);
	}

	IEnumerator HideQuiz() {
		yield return new WaitForSeconds(3);
		if (quizAnswer.text.ToLower() == answers[quizIndex]) {
			playerScript.Transmitte(nextRoom);
		} else {
			RandomizeDoors();
			doorPassed ++;
			playerScript.UpdateDoorPassed();
			playerScript.Reset();
			playerScript.printInfo();
		}
		quizAnswer.text = "";
		quizResult.text = "";
		quizMenu.SetActive(false);
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

	public void AnswerButton () {
		if (quizAnswer.text.ToLower() == answers[quizIndex]) {
			quizResult.text = "benar";
		} else {
			quizResult.text = "salah jawaban yang benar adalah \n" + answers[quizIndex].ToUpper();
		}
		StartCoroutine("HideQuiz");
	}

	public void ShowQuiz (int _nextRoom) {
		nextRoom = _nextRoom;
		quizIndex = Random.Range(0, questions.Length);
		quizQuestion.text = questions[quizIndex];
		quizMenu.SetActive(true);
	}

	public Player GetPlayer () {
		return playerScript;
	}

	public int GetDoor (string door) {
		return doors[currentRoom][int.Parse(door)];
	}
}
