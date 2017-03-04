using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerGravity : MonoBehaviour {
	public float gravity;
	public Rigidbody2D _rigidbody;

	void Awake() {
		_rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
	
	public void tryToFall() {
		Vector2 velocity = _rigidbody.velocity;
		velocity.y -= gravity * Time.fixedDeltaTime;
		_rigidbody.velocity = velocity;
	}
}
