using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]

public class BackgroundScroller : MonoBehaviour {

	private Camera _gameCamera;
	private float _height;
	private float _cameraHeight;

	void Awake () {
		_gameCamera = Camera.main;
		_cameraHeight = _gameCamera.orthographicSize;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		_height = renderer.sprite.bounds.size.y;
	}

	public void Init() {

	}
	
	void Update () {
		while (transform.position.y - _height / 2 > _gameCamera.transform.position.y + _cameraHeight) {
			transform.position = new Vector2(transform.position.x, transform.position.y - _cameraHeight * 2 - _height);
		}

		while (transform.position.y + _height / 2 < _gameCamera.transform.position.y - _cameraHeight) {
			transform.position = new Vector2(transform.position.x, transform.position.y + _cameraHeight * 2 + _height);
		}
	}
}
