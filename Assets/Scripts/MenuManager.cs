using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winText;
	[SerializeField] GameObject gameGroup;
	[SerializeField] GameObject goalGroup;
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
		SetGroupFalse();
		menuGroup.SetActive(true);
		Game.manager.PlayMusic(menuMusic);
	}

	void Update () {
		if (Input.GetButtonDown("Cancel") && !RoomManager.manager.IsWin() && !Game.manager.paused)
			ShowPauseMenu(!Game.manager.IsPaused());
	}

	void SetGroupFalse () {
		gameGroup.SetActive(false);
		menuGroup.SetActive(false);
		goalGroup.SetActive(false);
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
		SetGroupFalse();
		menuGroup.SetActive(true);
		Game.manager.PlayMusic(menuMusic);
	}

	public void GoToGoal () {
		SetGroupFalse();
		goalGroup.SetActive(true);
	}

	public void Play () {
		SetGroupFalse();
		RoomManager.manager.InitGame();
		gameGroup.SetActive(true);
	}
}
