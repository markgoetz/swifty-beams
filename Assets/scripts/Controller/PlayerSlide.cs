using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour {

	public float Duration;
	public float SlideVelocity;
	public float Cooldown;

	private bool _isSliding;

	private PlayerManager _manager;

	void Awake () {
		_manager = PlayerManager.GetInstance();
		_isSliding = false;
	}

	void Update () {
		if (Input.GetButtonDown("Slide") == true && _manager.CanSlide) {
			Debug.Log("Slide!");
			_isSliding = true;
			StartCoroutine(_FinishSlideCoroutine());
		}
	}

	void LateUpdate() {
		if (_isSliding) {
			Debug.Log("Sliding!");
			int directionModifier = _manager.FacingRight ? 1 : -1;
			float velocityX = directionModifier * SlideVelocity;
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, 0);
		}
	}

	private IEnumerator _FinishSlideCoroutine() {
		yield return new WaitForSeconds(Duration);
		_isSliding = false;
	}

	public void StopSlide() {
		_isSliding = false;
	}

	public bool IsSliding {
		get {
			return _isSliding;
		}
	}

	public static PlayerSlide GetInstance() {
		return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSlide>();
	}
}
