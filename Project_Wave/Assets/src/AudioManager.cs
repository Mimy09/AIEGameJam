using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Public variables
	[Range(0.0f,2.0f)]
	public float fade = 0.1f;
	[Range(0.0f,1.0f)]
	public float volume = 0.5f;
	public bool testFade = false;
	public List<AudioClip> audio = new List<AudioClip>();
	// Private variables
	private int currentAudio = 0;
	private int currentlyStopped = 1;
	private int currentlyPlaying = 0;
	private bool currentlyFading = false;
	private AudioSource[] audioSrc = new AudioSource[2];

	void Awake()
	{
		// Create Audio Sources
		this.audioSrc[0] = gameObject.AddComponent<AudioSource>() as AudioSource;
		this.audioSrc[1] = gameObject.AddComponent<AudioSource>() as AudioSource;
		// Set Default Audio Clips
		this.audioSrc[1].playOnAwake = false;
		this.audioSrc[1].volume = 0;
		this.audioSrc[1].clip = null;
		this.audioSrc[1].loop = true;
		// Set Default Audio Clips
		this.audioSrc[0].playOnAwake = false;
		this.audioSrc[0].volume = this.volume;
		this.audioSrc[0].clip = this.audio[0];
		this.audioSrc[0].loop = true;
		this.audioSrc[0].Play ();
	}

	void Update()
	{
		if (this.testFade)
		{
			this.testFade = false;
			PlayNextClip ((this.currentAudio + 1) % this.audio.Count);
		}
	}

	void FixedUpdate () {
		// check if fade required
		if (this.currentlyFading)
		{
			// Fade the audio volue
			AudioFadeIn(this.audioSrc [currentlyPlaying]);
			AudioFadeOut(this.audioSrc [currentlyStopped]);
			// check if complete
			if (this.audioSrc[currentlyStopped].volume > 0) return;
			if (this.audioSrc[currentlyPlaying].volume < this.volume) return;
			// set as complete
			this.currentlyFading = false;
		}
	}

	void AudioFadeIn(AudioSource src)
	{
		if (src.volume < this.volume) {
			src.volume += this.fade * Time.deltaTime;
		} else {
			src.volume = this.volume;
		}
	}

	void AudioFadeOut(AudioSource src)
	{
		if (src.volume > this.fade * Time.deltaTime) {
			src.volume -= this.fade * Time.deltaTime;
		} else {
			src.volume = 0;
		}
	}

	public void PlayNextClip(int index)
	{
		if (this.currentAudio == index) return;
		// set new values
		this.currentAudio = index;
		this.currentlyFading = true;
		this.currentlyStopped = this.currentlyPlaying;
		this.currentlyPlaying = (this.currentlyPlaying + 1) % 2;
		// set new clip to play
		this.audioSrc[this.currentlyPlaying].clip = this.audio[index];
		this.audioSrc[this.currentlyPlaying].Play ();
	}
}
