using UnityEngine;
using System.Collections;

public class RoomTrigger : MonoBehaviour {
	private GameObject manager;

	void Start () {
		manager = GameObject.FindGameObjectWithTag("LevelManager");
	}
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			manager.SendMessage ("Swap");
		}
	}
}
