using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(AudioSource))]
public class Laser : MonoBehaviour {

	public float velocity;
	public float delay;

	private Rigidbody2D _rigidBody;
	private AudioSource _audioSource;
	private string _direction;

	// Use this for initialization
	void Awake () {
		_rigidBody = GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (_rigidBody.velocity.x < 0 && transform.position.x < 0) {
			_rigidBody.velocity = new Vector2(0,0);
			transform.position = new Vector2(0, transform.position.y);
		}
		if (_rigidBody.velocity.x > 0 && transform.position.x > 0) {
			_rigidBody.velocity = new Vector2(0,0);
			transform.position = new Vector2(0, transform.position.y);
		}
	}

	public void Init(string direction) {
		StartCoroutine("SetDirectionCoroutine", direction);
	}
	
	IEnumerator SetDirectionCoroutine(string direction) {
		yield return new WaitForSeconds(delay);

		_audioSource.Play();
		if (direction == "left") {
			_rigidBody.velocity = new Vector2(-velocity, 0);
		}
		else if (direction == "right") {
			_rigidBody.velocity = new Vector2( velocity, 0);
		}
	}
}
