using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private PlayerMover _player;
	private LevelManager _level;
	private DiedMessageController _diedMessage;

	void Awake() {
		_player = PlayerMover.GetInstance();
		_level = LevelManager.GetInstance();
		_diedMessage = DiedMessageController.GetInstance();
	}

	void Start () {
		StartCoroutine(GameLoop());
	}
	
	private IEnumerator GameLoop() {
		while (true) {
			while (_player.IsAlive)
				yield return null;

			//
			_diedMessage.Show();

			while (!_diedMessage.IsDone) {
				yield return null;
			}

			_player.Init();
			_level.Init();
		}
	}
}
