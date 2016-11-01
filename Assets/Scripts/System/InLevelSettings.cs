using UnityEngine;
using UnityEngine.SceneManagement;

public class InLevelSettings : Settings {

	public string levelName;
	public static InLevelSettings Settings;

	public GameObject oneTimeUseItemPanel;

	public void Start() {
		Settings = this;
	}

	public bool SettingsOpen {
		get { return settingsPanel.activeSelf; }
	}

	public override void OpenSettings() {
		LevelManager.levelManager.PauseGame();
		LevelManager.levelManager.DeactivateButtons();
		LevelManager.levelManager.player.GetComponent<Pig>().ToggleActiveMovementButtons(false);
		settingsPanel.SetActive(true);
	}

	// Game will still be paused after settings are closed
	// TODO Change it so game unpauses automatically after closing settings
	public override void CloseSettings() {
		LevelManager.levelManager.ActivateButtons();
		settingsPanel.SetActive(false);
	}

	public void RestartLevel() {
		LevelManager.levelManager.UnpauseGame();
		SceneManager.LoadScene(levelName);
	}

	public void QuitLevel() {
		LevelManager.levelManager.UnpauseGame();
		SceneManager.LoadScene("LevelSelection");
	}

	public void OpenItems() {
		LevelManager.levelManager.PauseGame();
		LevelManager.levelManager.DeactivateButtons();
		LevelManager.levelManager.player.GetComponent<Pig>().ToggleActiveMovementButtons(false);
		oneTimeUseItemPanel.SetActive(true);
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