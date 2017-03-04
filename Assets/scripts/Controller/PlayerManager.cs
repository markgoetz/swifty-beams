using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public PlayerState _state;
	public BoxCollider2D standingHitbox;
	public BoxCollider2D slidingHitbox;
	private bool _facingRight = false;
	private PlayerMover _mover;
	private PlayerSlide _slide;

	void Awake() {
		_mover = PlayerMover.GetInstance();
		_slide = PlayerSlide.GetInstance();
	}

	void Update() {
		
	}

	void LateUpdate() {
		_setStateFromVelocity(_mover.Velocity);
	}

	public PlayerState State {
		get { return _state; }
	}

	public bool FacingRight {
		get { return _facingRight; }
	}

	private void _setStateFromVelocity(Vector2 velocity) {
		if (velocity.y < 0)
			this._state = PlayerState.Falling;
		else if (_slide.IsSliding == true)
			this._state = PlayerState.Sliding;
		else if (velocity.x != 0)
			this._state = PlayerState.Walking;
		else 
			this._state = PlayerState.Standing;

		if (velocity.x > 0)
			_facingRight = true;
		else if (velocity.x < 0)
			_facingRight = false;

		if (this._state == PlayerState.Falling)
			_slide.CancelSlide();

		if (this._state == PlayerState.Sliding) {
			standingHitbox.enabled = false;
			slidingHitbox.enabled = true;
		} else {
			standingHitbox.enabled = true;
			slidingHitbox.enabled = false;
		}
	}

	public bool CanWalk {
		get {
			if (_state == PlayerState.Sliding) return false;
			return true;
		}
	}

	public bool CanSlide {
		get {
			if (_state == PlayerState.Falling || _state == PlayerState.Sliding) return false;
			return true;
		}
	}

	public static PlayerManager GetInstance() {
		return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
	}
}

public enum PlayerState {
	Standing,
	Walking,
	Sliding,
	Falling
}
