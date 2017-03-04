using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathBehavior : MonoBehaviour {
	public GameObject deathEffect;
	private bool _isAlive;

	public void Die() {
		_isAlive = false;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<SpriteRenderer>().enabled = false;
		Transform.Instantiate(deathEffect, transform.position, Quaternion.identity);
	}

	public bool IsAlive {
		get {
			return _isAlive;
		}
	}

	public static PlayerDeathBehavior GetInstance() {
		return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeathBehavior>();
	}
}
