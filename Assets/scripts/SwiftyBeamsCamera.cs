using UnityEngine;
using System.Collections;

public class SwiftyBeamsCamera : MonoBehaviour {

	public float delayBeforeScroll;
	public float scrollTime;
	public float delayAfterScroll;

	private PlayerManager _player;

	void Awake() {
		_player = PlayerManager.GetInstance();
	}

	void Update() {
		transform.position = new Vector2(transform.position.x, _player.transform.position.y);
	}
}
