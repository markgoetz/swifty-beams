using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private PlayerState _state;
	private bool _facingRight = false;
	public StateTransition[] transitions;

	public void Awake() {
		_setUpTransitions();
	}

	public bool Transition(string action) {
		foreach (StateTransition t in transitions) {
			if (t.action == action && t.startState == _state) {
				_state = t.endState;
				return true;
			}
		}

		return false;
	}

	public PlayerState State {
		get { return _state; }
	}

	private void _setUpTransitions() {

	}

	public static PlayerController GetInstance() {
		return new PlayerController();
	}
}

public enum PlayerState {
	Standing,
	Walking,
	Sliding,
	Falling
}