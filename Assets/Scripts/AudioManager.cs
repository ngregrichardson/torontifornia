using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

	public AudioMixerGroup audioMixer;
	public Sound[] sounds;

	public static AudioManager instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup = audioMixer;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);

		if(s == null)
		{
			Debug.Log("Could not find sound");
			return;
		}

		s.source.volume = s.volume;

		s.source.Play();
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		s.source.Stop();
	}
}
