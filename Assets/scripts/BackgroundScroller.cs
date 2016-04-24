using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]

public class BackgroundScroller : MonoBehaviour {

	private Camera _gameCamera;
	private float _height;

	void Awake () {
		_gameCamera = Camera.main;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		_height = renderer.sprite.bounds.size.y;
	}
	
	void Update () {
		float camera_height = _gameCamera.orthographicSize;
		
		if (transform.position.y - _height / 2 > _gameCamera.transform.position.y + camera_height) {
			transform.position = new Vector2(transform.position.x, transform.position.y - camera_height * 2 - _height);
		}
	}
}
