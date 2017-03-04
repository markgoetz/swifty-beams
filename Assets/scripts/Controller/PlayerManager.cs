using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public PlayerState _state;
	private bool _facingRight = false;
	public StateTransition[] transitions;
	private PlayerWalk _walk;
	private PlayerSlide _slide;
	private PlayerCollisionManager _collision;

	void Awake() {
		_walk = gameObject.GetComponent<PlayerWalk>();
		_slide = gameObject.GetComponent<PlayerSlide>();
		_collision = gameObject.GetComponent<PlayerCollisionManager>();
	}

	void Update() {
		_checkForFalling();

		_checkForSlide();
		_checkForWalk();

		_checkForCollision();
		_setStateFromVelocity(_mover.Velocity);
	}

	void LateUpdate() {
		
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
