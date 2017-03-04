using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerManager))]
[RequireComponent (typeof(Rigidbody2D))]

public class PlayerWalk : MonoBehaviour {

	public float maxVelocity;
	public float groundAcceleration;
	public float groundDeceleration;
	public float gravity;
	public float airAcceleration;
	public float airDeceleration;

	public GameObject deathEffect;

	private Vector2 _velocity;

	private Vector2 _startPoint;

	private ScoreController _score;
	private bool _isFalling;

	private PlayerManager _manager;


	void Awake () {
		_startPoint = transform.position;
		_score = ScoreController.GetInstance();
		_manager = PlayerManager.GetInstance();
	}

	void Start() {
		Spawn();
	}

	public void Spawn() {
		_isFalling = false;
		transform.position = _startPoint;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	void Update () {
		if (_manager.CanWalk) {
			float x_direction = Input.GetAxisRaw ("Horizontal");

			if (x_direction != 0) {
				_velocity.x = Accelerate (_velocity.x, x_direction * getAcceleration (), maxVelocity);
			} else {
				_velocity.x = Decelerate (_velocity.x, getDeceleration());
			}
		}
	}
	
	void LateUpdate() {
		if (_manager.CanWalk)
			move (_velocity);
	}

	private float getAcceleration() {
		return (_isFalling) ? airAcceleration : groundAcceleration;
	}

	private float getDeceleration() {
		return (_isFalling) ? airDeceleration : groundDeceleration;
	}

	private void move(Vector2 velocity) {
		GetComponent<Rigidbody2D>().velocity = velocity;
	}

	public Vector2 Velocity {
		get { return _velocity; }
	}
	
	float Accelerate(float initial_velocity, float amount, float max_velocity) {
		return Mathf.Clamp (initial_velocity + amount * Time.deltaTime, -max_velocity, max_velocity);
	}

	float Decelerate(float initial_velocity, float amount) {
		if (initial_velocity > 0) {
			return Mathf.Max (0, initial_velocity - amount * Time.deltaTime);
		} else {
			return Mathf.Min (0, initial_velocity + amount * Time.deltaTime);		
		}
	}
	
	void SetMoving(bool moving_value) {
		//_moving = moving_value;
		_velocity = Vector2.zero;
	}
}