using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {
	private PlayerController _player;
	private float _offset;

	void Awake() {
		_player = PlayerController.GetInstance();
	}

	void Start() {
		_offset = transform.position.y - _player.transform.position.y;
	}

	void Update() {
		transform.position = new Vector3(transform.position.x, _player.transform.position.y + _offset, transform.position.z);
	}
}
