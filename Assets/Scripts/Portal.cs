using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	void OnMouseDown () {
		Player player = RoomManager.manager.player;
		if (player != null && !Game.manager.IsPaused()) {
			if (!player.IsMoving())
				player.Move(transform.position);
		}
	}
}
