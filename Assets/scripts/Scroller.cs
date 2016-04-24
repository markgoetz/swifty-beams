using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {
	public float scrollHeight;

	public void Scroll(float time) {
		StartCoroutine("ScrollCoroutine", time);
	}
	
	public IEnumerator ScrollCoroutine(float scrollTime) {		
		float elapsed_time = 0;
		Vector3 start_position = transform.position;
		Vector3 target_position = transform.position - new Vector3(0,scrollHeight,0);
		
		while (elapsed_time < scrollTime) {
			transform.position = Vector3.Lerp (start_position, target_position, elapsed_time / scrollTime);
			yield return null;
			elapsed_time += Time.deltaTime;
		}
	}
}
