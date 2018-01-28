using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winText;
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
		if (Input.GetButtonDown("Cancel") && !RoomManager.manager.IsWin() && !Game.manager.paused)
			ShowPauseMenu(!Game.manager.IsPaused());
	}

	public void ShowWinText (bool show) {
		ShowPauseMenu(show);
		if (winText != null)
			winText.SetActive(Game.manager.pausedWithTime);
	}

	public void ShowPauseMenu (bool show) {
		Game.manager.pauseGameWithTime(show);
		if (pauseMenu != null)
			pauseMenu.SetActive(Game.manager.pausedWithTime);
	}

	public void PlayButton () {
		ShowPauseMenu(!Game.manager.pausedWithTime);
		if (RoomManager.manager.IsWin())
			ResetButton();
	}

	public void ResetButton () {
		ShowWinText(false);
		RoomManager.manager.InitGame();
	}

	public void MusicButton () {
 		Game.manager.musicOn = !Game.manager.musicOn;
		Game.manager.PlayMusic();
	}

	public void MenuButton () {
		ShowWinText(false);
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
