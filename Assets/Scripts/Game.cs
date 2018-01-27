using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	public static Game manager;

	public bool audioOn;
	public bool musicOn;
	public bool paused;

	AudioSource musicSource;

	void Awake () {
		if (manager == null)
			manager = this;
		else if (manager != this)
			Destroy(gameObject);

		audioOn = true;
		musicOn = true;

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

	public void StopGame (bool stop) {
		paused = stop;
		Time.timeScale = paused ? 0 : 1;
	}
}
