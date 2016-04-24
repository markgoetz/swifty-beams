using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerDeathEffect : MonoBehaviour {
	private AudioSource _audioSource;
	private ParticleSystem _particleSystem;

	void Awake() {
		_audioSource = GetComponent<AudioSource>();
		_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Trigger() {
		_audioSource.Play();
		_particleSystem.Play();
	}
}
