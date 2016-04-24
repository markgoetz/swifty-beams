using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreView : MonoBehaviour {
	public Text scoreText;
	public Text bestScoreText;

	public void UpdateScore (int score, int bestScore) {
		scoreText.text = "SCORE: " + score;
		bestScoreText.text = "BEST: " + bestScore;
	}
}
