using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerDeathBehavior))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerCollisionManager : MonoBehaviour {
	private ScoreController _score;
	private PlayerDeathBehavior _death;

	private Rigidbody2D _rigidbody;
	private Rect _hitbox;

	private int _traces = 5;
	private float _hitboxGap = .01f;
	private LayerMask _layerMask;

	void Awake() {
		_score = ScoreController.GetInstance();
		_death = PlayerDeathBehavior.GetInstance();
		_rigidbody = gameObject.GetComponent<Rigidbody2D>();
		_layerMask = 1 << LayerMask.NameToLayer ("Block");
	}

	void LateUpdate() {
		_checkHorizontalCollision();
		_checkVerticalCollision();
	}

	void _checkHorizontalCollision() {
		Vector2 _velocity = _rigidbody.velocity;
		_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);

		if (_velocity.x != 0) {
			// raycast forward / back to see if you hit anything.
			Vector2 amount_to_move = _velocity.x * Time.deltaTime;
			Vector2 direction = new Vector2 (Mathf.Sign (_velocity.x), 0);
			Vector2 start_point = new Vector2(_hitbox.center.x, _hitbox.yMin + _hitboxGap);
			Vector2 end_point   = new Vector2(_hitbox.center.x, _hitbox.yMax - _hitboxGap);

			for (int i = 0; i <= _traces; i++) {
				Vector2 origin = Vector2.Lerp (start_point, end_point, i / (_traces - 1));
				RaycastHit2D hit = Physics2D.Raycast (origin, direction, Mathf.Abs (amount_to_move) + _hitbox.width / 2, _layerMask);

				if (hit.collider != null) {
					float distance = Mathf.Abs (hit.point.x - origin.x);
					distance -= _hitbox.width / 2;
					transform.Translate(direction * distance);
					_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);
					_velocity.x = 0;
					break;
				}
			}
		}
	}

	void _checkVerticalCollision() {

		Vector2 _velocity = _rigidbody.velocity;

		// do a raycast down to determine if you would hit anything this frame.		
		Vector2 amount_to_move = _velocity.y * Time.deltaTime;
		Vector2 direction = new Vector2 (0, -1);
		Vector2 start_point = new Vector2(_hitbox.xMin + _hitboxGap, _hitbox.center.y);
		Vector2 end_point   = new Vector2(_hitbox.xMax - _hitboxGap, _hitbox.center.y);
		
		for (int i = 0; i <= _traces; i++) {
			Vector2 origin = Vector2.Lerp (start_point, end_point, i / (_traces - 1));
			
			RaycastHit2D hit = Physics2D.Raycast (origin, direction, Mathf.Abs (amount_to_move) + _hitbox.height / 2, _layerMask);
			
			if (hit.collider != null) {
				float distance = Mathf.Abs (hit.point.y - origin.y);
				distance -= _hitbox.height / 2;
				_velocity.y = 0;
				transform.Translate (direction * distance);
				_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);
				break;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Laser") {
			_death.Die();
		}
		
		else if (collision.gameObject.tag == "LaserTrigger") {
			LevelManager level = LevelManager.GetInstance();
			level.AddRowWithLaser(collision.gameObject.GetComponent<LaserTrigger>());
		}
		
		else if (collision.gameObject.tag == "ScoreTrigger") {
			_score.AddScore();
		}		
	}
}
