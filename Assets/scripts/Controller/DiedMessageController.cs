using UnityEngine;
using System.Collections;

public class DiedMessageController : MonoBehaviour {
	private bool _isDone;


	void Start () {
		_isDone = false;
	}

	public void Show() {
		_isDone = false;

	}

	public bool IsDone {
		get { return _isDone; }
	}

	public static DiedMessageController GetInstance() {
		return GameObject.FindGameObjectWithTag("Died Message").GetComponent<DiedMessageController>();
	}
}
