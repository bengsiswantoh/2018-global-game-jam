using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static Game manager;

	public bool audioOn;
	public bool musicOn;
	public bool pausedWithTime;
	public bool paused;

	AudioSource musicSource;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		audioOn = true;
		musicOn = true;
		pausedWithTime = false;
		paused = false;

		musicSource = GetComponent<AudioSource>();
	}

	public void PlayMusic (AudioClip source = null) {
		if (source != null)
			musicSource.clip = source;

		if (musicOn && musicSource.clip != null) {
			musicSource.Play();
		} else {
			musicSource.Stop();
		}
	}

	public void pauseGameWithTime (bool stop) {
		pausedWithTime = stop;
		Time.timeScale = pausedWithTime ? 0 : 1;
	}

	public bool IsPaused () {
		return paused || pausedWithTime;
	}
}
