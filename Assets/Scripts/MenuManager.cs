using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject winMenu;

	public static MenuManager manager;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);
	}

	void Update () {
		if (Input.GetButtonDown("Cancel") && !RoomManager.manager.IsWin())
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
	}

	public void ResetButton () {
		ShowPauseMenu(false);
		RoomManager.manager.InitGame();
		RoomManager.manager.playerScript.Reset();
	}

	public void MusicButton () {
 		Game.manager.musicOn = !Game.manager.musicOn;
		Game.manager.PlayMusic();
	}

	public void MenuButton () {

	}
}
