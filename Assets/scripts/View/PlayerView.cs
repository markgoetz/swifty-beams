using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour {
	private Animator _animator;
	private bool _facingRight = false;

	void Awake() {
		_animator = GetComponent<Animator>();
	}

	public void UpdateVelocity(Vector2 velocity, bool is_falling) {
		if (velocity.x < 0)
			_facingRight = false;
		else if (velocity.x > 0)
			_facingRight = true;

		_animator.SetBool("is_falling", is_falling);
		_animator.SetBool("moving", Mathf.Abs(velocity.x) > .01f);
		_animator.SetBool("facing_right", _facingRight);
	}
}
