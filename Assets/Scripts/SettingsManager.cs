using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	#region Singleton
	public static SettingsManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}
	#endregion

	public AudioMixer audioMixer;

	public Toggle fullscreenToggle;
	public Slider volumeSlider;
	public Dropdown resolutionDropdown;
	public Dropdown graphicsDropdown;

	int resolution = 0;

	Resolution[] resolutions;

	void Start()
	{
		if(instance == null)
		{
			instance = this;
		}else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		resolutions = Screen.resolutions;

		resolutionDropdown.ClearOptions();

		List<string> options = new List<string>();

		int currentResolutionIndex = 0;
		for(int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);
			if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();

		if (PlayerPrefs.GetInt("isFullscreen", 0).Equals(0))
		{
			fullscreenToggle.isOn = false;
		} else
		{
			fullscreenToggle.isOn = true;
		}
		volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.75f);
		graphicsDropdown.value = PlayerPrefs.GetInt("quality", 0);
		graphicsDropdown.RefreshShownValue();
	}

	public void SetVolume(float _volume)
	{
		Debug.Log("volume changing");
		audioMixer.SetFloat("volume", _volume);
		PlayerPrefs.SetFloat("volume", _volume);
	}

	public void SetQuality(int quality)
	{
		QualitySettings.SetQualityLevel(quality);
		PlayerPrefs.SetInt("quality", quality);
	}

	public void SetResolution(int index)
	{
		Resolution res = resolutions[index];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);
		PlayerPrefs.SetInt("resolution", resolution);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
		if(isFullscreen)
		{
			PlayerPrefs.SetInt("isFullscreen", 1);
		}else
		{
			PlayerPrefs.SetInt("isFullscreen", 0);
		}
	}
}
