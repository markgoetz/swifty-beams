using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour {

	public float Duration;
	public float SlideVelocity;
	public float Cooldown;

	private bool _isSliding;
	private bool _canSlide;

	private PlayerManager _manager;

	void Awake () {
		_manager = PlayerManager.GetInstance();
		_isSliding = false;
		_canSlide = true;
	}

	void Update () {
		if (Input.GetButtonDown("Slide") == true && _canSlide && _manager.CanSlide) {
			_isSliding = true;
			_canSlide = false;
			StartCoroutine(_FinishSlideCoroutine());
		}
	}

	void LateUpdate() {
		if (_isSliding) {
			int directionModifier = _manager.FacingRight ? 1 : -1;
			float velocityX = directionModifier * SlideVelocity;
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, 0);
		}
	}

	private IEnumerator _FinishSlideCoroutine() {
		yield return new WaitForSeconds(Duration);
		_isSliding = false;
		yield return new WaitForSeconds(Cooldown);
		_canSlide = true;
	}

	public void CancelSlide() {
		_isSliding = false;
		_canSlide = true;
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
