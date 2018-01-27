using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static Game game;

	public static bool audioOn;
	public static bool musicOn;

	void Awake () {
		if (game == null)
			game = this;
		else if (game != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		audioOn = true;
		musicOn = true;
	}
}
