using UnityEngine;
using System.Collections;

public class SwiftyBeamsCamera : MonoBehaviour {

	public float delayBeforeScroll;
	public float scrollTime;
	public float delayAfterScroll;

	private PlayerController _player;

	void Awake() {
		_player = PlayerController.GetInstance();
	}

	void Update() {
		transform.position = new Vector2(transform.position.x, _player.transform.position.y);
	}
}
