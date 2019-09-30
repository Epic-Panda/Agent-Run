using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

	public GameObject menuUI;
	public AudioMixer audioMixer;

	public Slider audioSlider;
	public Slider SFXSlider;
	public Slider sensitivitySlider;

	void Start ()
	{
		float vol;

		if (audioMixer.GetFloat ("Volume", out vol))
			audioSlider.value = vol;

		if (audioMixer.GetFloat ("SFX volume", out vol))
			SFXSlider.value = vol;

		sensitivitySlider.value = PlayerPrefs.GetFloat ("mouseSensitivity", 4f);
	}

	public void SetVolume (float vol)
	{
		audioMixer.SetFloat ("Volume", vol);
	}

	public void SetSFXVolume (float sfxVol)
	{
		audioMixer.SetFloat ("SFX volume", sfxVol);
	}

	public void SetSensitivity (float sensitivity)
	{
		PlayerPrefs.SetFloat ("mouseSensitivity", sensitivity);
	}

	public void GoBack ()
	{
		gameObject.SetActive (false);
		menuUI.SetActive (true);
	}
}
