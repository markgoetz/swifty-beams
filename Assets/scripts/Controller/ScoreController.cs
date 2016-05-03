using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ScoreView))]
public class ScoreController : MonoBehaviour {
	private int _score;
	private int _bestScore = 0;

	private ScoreView _view;

	void Awake() {
		_view = GetComponent<ScoreView>();
	}

	public void ResetScore() {
		_score = 0;
		_view.UpdateScore(_score, _bestScore);
	}

	public void AddScore() {
		_score++;
		if (_score > _bestScore) _bestScore = _score;

		_view.UpdateScore(_score, _bestScore);
	}

	void Start () {
		ResetScore();
	}

	public static ScoreController GetInstance() {
		return GameObject.FindGameObjectWithTag("Score Counter").GetComponent<ScoreController>();
	}
}
