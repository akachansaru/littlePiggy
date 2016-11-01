using UnityEngine;
using UnityEngine.SceneManagement;

public class InLevelSettings : Settings {

	public string levelName;
	public static InLevelSettings Settings;

	public void Start() {
		Settings = this;
	}

	public bool SettingsOpen {
		get { return settingsPanel.activeSelf; }
	}

	public override void CloseSettings() {
		ClosePanel(settingsPanel);
	}

	public void RestartLevel() {
		LevelManager.levelManager.UnpauseGame();
		SceneManager.LoadScene(levelName);
	}

	public void QuitLevel() {
		LevelManager.levelManager.UnpauseGame();
		SceneManager.LoadScene("LevelSelection");
	}

	public void OpenPanel(GameObject panel) {
		LevelManager.levelManager.PauseGame();
		LevelManager.levelManager.DeactivateButtons();
		LevelManager.levelManager.player.GetComponent<Pig>().ToggleActiveMovementButtons(false);
		panel.SetActive(true);
	}

	public void ClosePanel(GameObject panel) {
		LevelManager.levelManager.UnpauseGame();
		LevelManager.levelManager.ActivateButtons();
		panel.SetActive(false);
	}
}