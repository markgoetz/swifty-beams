using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerView))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
public class PlayerMover : MonoBehaviour {

	public float maxVelocity;
	public float groundAcceleration;
	public float groundDeceleration;
	public float gravity;
	public float airAcceleration;
	public float airDeceleration;

	public GameObject deathEffect;

	private PlayerView _view;

	private Vector2 _velocity;
	private Rect _hitbox;

	private float _hitboxGap = .01f;

	private Vector2 _startPoint;

	private ScoreController _score;
	private bool _isFalling;
	private bool _isAlive;


	// Use this for initialization
	void Awake () {
		//position = gameObject.transform.position;
		//velocity = new Vector2 (0, 0);
		_view = GetComponent<PlayerView> ();
		_startPoint = transform.position;
		_score = ScoreController.GetInstance();
		_isAlive = true;
	}

	void Start() {
		Init();
	}

	public void Init() {
		_isFalling = false;
		_isAlive = true;
		transform.position = _startPoint;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	void Update () {
		if (!_isAlive) return;
	
		_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);
		Vector2 direction;
		float amount_to_move;
		Vector2 start_point;
		Vector2 end_point;
		Vector2 origin;
		LayerMask layer_mask = 1 << LayerMask.NameToLayer ("Block");
		int traces = 3;	

		float x_direction = Input.GetAxisRaw ("Horizontal");

		if (x_direction != 0) {
			_velocity.x = Accelerate (_velocity.x, x_direction * getAcceleration (), maxVelocity);
		} else {
			_velocity.x = Decelerate (_velocity.x, getDeceleration());
		}

		if (_velocity.x != 0) {
			// raycast forward / back to see if you hit anything.
			amount_to_move = _velocity.x * Time.deltaTime;
			direction = new Vector2 (Mathf.Sign (_velocity.x), 0);
			start_point = new Vector2(_hitbox.center.x, _hitbox.yMin + _hitboxGap);
			end_point   = new Vector2(_hitbox.center.x, _hitbox.yMax - _hitboxGap);

			for (int i = 0; i <= traces; i++) {
				origin = Vector2.Lerp (start_point, end_point, i / (traces - 1));
				RaycastHit2D hit = Physics2D.Raycast (origin, direction, Mathf.Abs (amount_to_move) + _hitbox.width / 2, layer_mask);

				if (hit.collider != null) {
					float distance = Mathf.Abs (hit.point.x - origin.x);
					distance -= _hitbox.width / 2;
					//distance -= hitbox_gap;
					transform.Translate(direction * distance);
					_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);
					_velocity.x = 0;
					break;
				}
			}
		}
	
		_velocity.y -= gravity * Time.deltaTime;
		// do a raycast down to determine if you would hit anything this frame.		
		amount_to_move = _velocity.y * Time.deltaTime;
		direction = new Vector2 (0, -1);
		start_point = new Vector2(_hitbox.xMin + _hitboxGap, _hitbox.center.y);
		end_point   = new Vector2(_hitbox.xMax - _hitboxGap, _hitbox.center.y);
		
		for (int i = 0; i <= traces; i++) {
			origin = Vector2.Lerp (start_point, end_point, i / (traces - 1));
			
			RaycastHit2D hit = Physics2D.Raycast (origin, direction, Mathf.Abs (amount_to_move) + _hitbox.height / 2, layer_mask);
			
			if (hit.collider != null) {
				float distance = Mathf.Abs (hit.point.y - origin.y);
				distance -= _hitbox.height / 2;
				//distance -= hitbox_gap;
				_velocity.y = 0;
				transform.Translate (direction * distance);
				_hitbox = new Rect (GetComponent<Collider2D>().bounds.min.x + _hitboxGap, GetComponent<Collider2D>().bounds.min.y + _hitboxGap, GetComponent<Collider2D>().bounds.size.x - _hitboxGap * 2, GetComponent<Collider2D>().bounds.size.y - _hitboxGap * 2);
				break;
			}
		}
	}
	
	void LateUpdate() {

	
		move (_velocity);
		_view.UpdateVelocity(_velocity, _isFalling);
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

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Laser") {
			Die ();
		}
		
		else if (collision.gameObject.tag == "LaserTrigger") {
			GameObject level = GameObject.FindGameObjectsWithTag("LevelManager")[0];
			level.SendMessage("AddRowWithLaser", collision.gameObject.GetComponent<LaserTrigger>());
		}
		
		else if (collision.gameObject.tag == "ScoreTrigger") {
			AddScore();
		}		
	}

	void Die() {
		_velocity = Vector2.zero;
		_isAlive = false;
		GetComponent<SpriteRenderer>().enabled = false;
		Transform.Instantiate(deathEffect, transform.position, Quaternion.identity);
		//gameObject.SetActive (false);
	}
	
	void ResetPosition() {
		transform.position = _startPoint;
	}
	
	void AddScore() {
		_score.AddScore();
	}
	
	void SetMoving(bool moving_value) {
		//_moving = moving_value;
		_velocity = Vector2.zero;
	}

	public bool IsAlive {
		get {
			return _isAlive;
		}
	}

	public static PlayerMover GetInstance() {
		return GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMover>();
	}
}