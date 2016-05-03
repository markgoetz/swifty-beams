using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiedMessageController : MonoBehaviour {
	public CanvasGroup message;

	private bool _isDone;


	void Start () {
		_isDone = false;
		message.alpha = 0;
		message.interactable = false;
	}

	public void Show() {
		_isDone = false;
		message.alpha = 1;
		message.interactable = true;
	}

	public void OnButtonClick() {
		_isDone = true;
		message.alpha = 0;
		message.interactable = false;
	}

	public bool IsDone {
		get { return _isDone; }
	}

	public static DiedMessageController GetInstance() {
		return GameObject.FindGameObjectWithTag("Died Message").GetComponent<DiedMessageController>();
	}
}
