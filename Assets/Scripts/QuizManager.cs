using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

	[SerializeField] GameObject quizMenu;
	[SerializeField] Text quizQuestion;
	[SerializeField] Text quizResult;
	[SerializeField] InputField quizAnswer;
	[SerializeField] AudioClip quizMusic;

	public static QuizManager manager;

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
		"Nama binatang yang diulang '_ _ R _ _ _'",
		"Pasang bendera di 'T _ _ _ _'",
		"Puasa di bulan '_ _ _ _ _ H _ N'",
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

	void Awake() {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);
	}

	public void AnswerButton () {
		if (quizAnswer.text.ToLower() == answers[quizIndex]) {
			quizResult.text = "Benar";
		} else {
			// quizResult.text = "Salah jawaban yang benar adalah \n" + answers[quizIndex].ToUpper();
			quizResult.text = "Salah";
			quizResult.text += "\n Portal akan berubah";
		}
		StartCoroutine("HideQuiz");
	}

	public void ShowQuiz (int _nextRoom) {
		quizAnswer.text = "";
		quizResult.text = "";
		// Random question
		quizIndex = Random.Range(0, questions.Length);
		quizQuestion.text = questions[quizIndex];
		quizMenu.SetActive(true);
		RoomManager.manager.nextRoom = _nextRoom;
		Game.manager.PlayMusic(quizMusic);
		Game.manager.paused = true;
	}

	IEnumerator HideQuiz() {
		yield return new WaitForSeconds(3);
		if (quizAnswer.text.ToLower() == answers[quizIndex]) {
			RoomManager.manager.player.Transmitte(RoomManager.manager.nextRoom);
		} else {
			RoomManager.manager.ReinitDoors();
		}
		quizMenu.SetActive(false);
		Game.manager.PlayMusic(RoomManager.manager.GetCurrentMusic());
		Game.manager.paused = false;
	}
}
