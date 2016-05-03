using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]

public class PlayerAnimator : MonoBehaviour {
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void SetVelocity(Vector2 velocity) {
		animator.SetFloat("x_velocity", velocity.x);
		animator.SetFloat("y_velocity", velocity.y);
	}
}
