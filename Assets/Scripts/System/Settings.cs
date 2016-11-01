using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {

	public GameObject settingsPanel;
	public Slider musicVolumeSlider;
	public AudioSource music;
	public Toggle musicMutedToggle;

	void Start() {
		GlobalControl.Instance.Load();
		if (musicMutedToggle) {
			musicMutedToggle.isOn = GlobalControl.Instance.savedData.musicMuted;
		}
		if (musicVolumeSlider) {
			musicVolumeSlider.value = GlobalControl.Instance.savedData.musicVolume;
		}	
	}

	public virtual void OpenSettings() {
		settingsPanel.SetActive(true);
	}

	public void DeactivateButton(GameObject button) {
		button.SetActive (false);
	}

	public void ActivateButton(GameObject button) {
		button.SetActive (true);
	}
		
	public virtual void CloseSettings() {
		settingsPanel.SetActive(false);
	}

	public void AdjustMusicVolume() {
		Debug.Log("Volume adjusted.");
		music.volume = musicVolumeSlider.value;
		GlobalControl.Instance.savedData.musicVolume = music.volume;
		GlobalControl.Instance.Save();
	}

	public void MuteMusic() {
		Debug.Log("Mute toggled.");
		music.mute = !music.mute;
		GlobalControl.Instance.savedData.musicMuted = music.mute;
		GlobalControl.Instance.Save();
	}
}