using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour {

	// public string levelName;
	public GameObject settingsPanel;
	public Slider musicVolumeSlider;
	public AudioSource introMusic;
	public Toggle musicMutedToggle;

	// Use this for initialization
	void Start() {
		GlobalControl.Instance.Load();
		if (musicMutedToggle) {
			musicMutedToggle.isOn = GlobalControl.Instance.savedData.musicMuted;
		}
		if (musicVolumeSlider) {
			musicVolumeSlider.value = GlobalControl.Instance.savedData.musicVolume;
		}
	}
		
	public virtual void CloseSettings() {
		settingsPanel.SetActive(false);
	}

	// public void RestartLevel() {
	// 	LevelManager.levelManager.UnpauseGame();
	// 	SceneManager.LoadScene(levelName);
	// }

	// public void QuitLevel() {
	// 	LevelManager.levelManager.UnpauseGame();
	// 	// GlobalControl.Instance.savedData
	// 	SceneManager.LoadScene("MainMenu");
	// }

	public void AdjustMusicVolume() {
		Debug.Log("Volume adjusted");
		introMusic.volume = musicVolumeSlider.value;
		GlobalControl.Instance.savedData.musicVolume = introMusic.volume;
		GlobalControl.Instance.Save();
	}

	public void MuteMusic() {
		Debug.Log("Mute toggled");
		introMusic.mute = !introMusic.mute;
		GlobalControl.Instance.savedData.musicMuted = introMusic.mute;
		GlobalControl.Instance.Save();
	}
}