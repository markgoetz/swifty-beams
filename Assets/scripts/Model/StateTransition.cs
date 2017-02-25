using UnityEngine;
using System.Collections;

[System.Serializable]
public class StateTransition {
	public string action;
	public PlayerState startState;
	public PlayerState endState;
}
