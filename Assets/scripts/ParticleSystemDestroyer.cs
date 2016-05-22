using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleSystemDestroyer : MonoBehaviour {

	private ParticleSystem ps;

	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	void Update () {
		if (!ps.IsAlive()) {
			// deactivate the object first to prevent minor graphical glitches while the object is being destroyed
			gameObject.SetActive (false);
			Destroy (gameObject);
		}
	}
}
