using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winMenu;
	[SerializeField] GameObject gameGroup;
	[SerializeField] GameObject menuGroup;
	[SerializeField] AudioClip menuMusic;

	public static MenuManager manager;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);
	}

	void Start () {
		gameGroup.SetActive(false);
		Game.manager.PlayMusic(menuMusic);
	}

	void Update () {
		if (Input.GetButtonDown("Cancel") && !RoomManager.manager.IsWin() && !QuizManager.manager.IsActive())
			ShowPauseMenu(!Game.manager.paused);
	}

	public void ShowWinMenu (bool show) {
		ShowPauseMenu(show);
		if (winMenu != null)
			winMenu.SetActive(Game.manager.paused);
	}

	public void ShowPauseMenu (bool show) {
		Game.manager.StopGame(show);
		if (pauseMenu != null)
			pauseMenu.SetActive(Game.manager.paused);
	}

	public void PlayButton () {
		ShowPauseMenu(!Game.manager.paused);
		if (RoomManager.manager.IsWin())
			ResetButton();
	}

	public void ResetButton () {
		ShowWinMenu(false);
		RoomManager.manager.InitGame();
	}

	public void MusicButton () {
 		Game.manager.musicOn = !Game.manager.musicOn;
		Game.manager.PlayMusic();
	}

	public void MenuButton () {
		ShowWinMenu(false);
		gameGroup.SetActive(false);
		menuGroup.SetActive(true);
		Game.manager.PlayMusic(menuMusic);
	}

	public void Play () {
		RoomManager.manager.InitGame();
		menuGroup.SetActive(false);
		gameGroup.SetActive(true);
	}
}
