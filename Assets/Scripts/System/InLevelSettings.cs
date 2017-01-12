using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InLevelSettings : Settings {
	public static InLevelSettings Settings;
	public static bool paused = false;

	public GameObject player;
	public string levelName;
	public GameObject itemPanel;
	public GameObject pauseScreen;
	public Button settingsButton;
	public Button pauseButton;
	public Button unpauseButton;
	public Button itemButton;

	public void Start() {
		GlobalControl.Instance.Load();
		base.SetSavedMusicSettings ();
		Settings = this;
	}

	public bool SettingsOpen {
		get { return settingsPanel.activeSelf; }
	}

	public override void OpenSettings() {
		OpenPanel(settingsPanel);
	}

	public override void CloseSettings() {
		ClosePanel(settingsPanel);
	}

	public void OpenItems() {
		OpenPanel(itemPanel);
	}

	public void CloseItems() {
		ClosePanel(itemPanel);
	}

	public void RestartLevel() {
		UnpauseGame();
		SceneManager.LoadScene(levelName);
	}

	public void QuitLevel() {
		UnpauseGame();
		SceneManager.LoadScene("LevelSelection");
	}

	public void PauseGame() {
		paused = true;
		if (pauseScreen) {
			pauseScreen.SetActive (true);
		}
		if (pauseButton) {
			pauseButton.gameObject.SetActive (false);
			unpauseButton.gameObject.SetActive (true);
		}
		Time.timeScale = 0f;
		player.GetComponent<PigControlInput>().ToggleActiveMovementButtons(false);
        Debug.Log("Paused");
	}

	public void UnpauseGame() {
		paused = false;
		if (pauseScreen) {
			pauseScreen.SetActive (false);
		}
		if (pauseButton) {
			unpauseButton.gameObject.SetActive (false);
			pauseButton.gameObject.SetActive (true);
		}
		Time.timeScale = 1f;
		player.GetComponent<PigControlInput>().ToggleActiveMovementButtons(true);
	}

	void OpenPanel(GameObject panel) {
		PauseGame();
		DeactivateButtons();
		panel.SetActive(true);
	}

	void ClosePanel(GameObject panel) {
		UnpauseGame();
		ActivateButtons();
		panel.SetActive(false);
	}

	public void DeactivateButtons() {
		if (settingsButton) {
			settingsButton.gameObject.SetActive (false);
		}
		if (itemButton) {
			itemButton.gameObject.SetActive (false);
		}
		if (pauseButton) {
			pauseButton.gameObject.SetActive (false);
			unpauseButton.gameObject.SetActive (false);
		}
	}

	public void ActivateButtons() {
		if (settingsButton) {
			settingsButton.gameObject.SetActive (true);
		}
		if (itemButton) {
			itemButton.gameObject.SetActive (true);
		}
		if (pauseButton) {
			pauseButton.gameObject.SetActive (true);
		}
	}
}