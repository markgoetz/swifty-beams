using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour {
	private Animator _animator;
	private PlayerManager _player;

	void Awake() {
		_animator = GetComponent<Animator>();
		_player = PlayerManager.GetInstance();
	}

	void Update() {
		_animator.SetBool("facing_right", _player.FacingRight);
	    bool walking = false;
	    bool standing = false;
	    bool falling = false;
	    bool sliding = false;

		switch (_player.State) {
			case PlayerState.Walking:
				walking = true;
				break;
			case PlayerState.Standing:
				standing = true;
				break;
			case PlayerState.Falling:
				falling = true;
				break;
			case PlayerState.Sliding:
				sliding = true;
				break;
		}

	    _animator.SetBool("walking", walking);
	    _animator.SetBool("standing", standing);
	    _animator.SetBool("falling", falling);
	    _animator.SetBool("sliding", sliding);
	}
}
