using UnityEngine;
using System.Collections;

public class LaserTrigger : MonoBehaviour {

	private const float DOUBLE_CHANCE = .04f;
	private bool left_side;
	private bool right_side;

	// Use this for initialization
	void Awake () {
		float initializer = Random.value;
		if (initializer < DOUBLE_CHANCE) {
			left_side = true;
			right_side = true;	
		}
		else {
			if (Random.value < .5) {
				left_side = true;
				right_side = false;
			}
			else {
				left_side = false;
				right_side = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool LeftSide {
		get { return left_side; }
	}
	
	public bool RightSide {
		get { return right_side; }
	}
}
