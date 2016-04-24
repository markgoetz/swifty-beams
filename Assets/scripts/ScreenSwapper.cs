using UnityEngine;
using System.Collections;

public class ScreenSwapper : MonoBehaviour {
	public float delayBeforeSwap;
	public float swapTime;
	public float delayAfterSwap;
	
	public GameObject currentRoom;
	public GameObject backRoom;
	
	private GameObject player;
	private Camera game_camera;
	
	private bool is_swapping = false;
	
	public void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		game_camera = Camera.main;
	}
	
	public void Swap() {
		if (is_swapping) return;
		StartCoroutine("SwapCoroutine");
	}
	
	private IEnumerator SwapCoroutine() {
		is_swapping = true;
		player.SendMessage ("SetMoving", false);
		
		backRoom.transform.position = new Vector3(
			backRoom.transform.position.x,
			currentRoom.transform.position.y - 320,
			backRoom.transform.position.z
		);
		
		GameObject temp = backRoom;
		backRoom = currentRoom;
		currentRoom = temp;
	
		yield return new WaitForSeconds(delayBeforeSwap);
		
		player.SendMessage ("Scroll", swapTime);
		game_camera.SendMessage ("Scroll", swapTime);
		yield return new WaitForSeconds(swapTime + delayAfterSwap);
		
		player.SendMessage ("SetMoving", true);
		is_swapping = false;
		yield break;
	}
}
