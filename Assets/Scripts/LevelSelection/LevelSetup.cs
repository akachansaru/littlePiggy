using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class LevelSetup : MonoBehaviour {

	public GameObject paymentPanel;
	public GameObject paymentPrompt;
	public InputField paymentInputField;
	public GameObject placeholder;

	public static char delimiter = '_';

	private string levelName;
	private int minLevelPayment;
	private int maxLevelPayment;
	private Text paymentPromptText;

	void Start() {
		paymentPromptText = paymentPrompt.GetComponent<Text>();
	}

	// The input has to be in this format: LevelOne_0_10 for the level "LevelOne" with a min payment of 0 and max of 10
	public void SelectLevelPayment(string levelNameAndPayment) {
		LevelManager.levelManager.PauseGame();
		LevelManager.levelManager.DeactivateButtons();
		paymentPanel.SetActive(true);
		string[] split = levelNameAndPayment.Split(delimiter);
		levelName = split[0];
		minLevelPayment = Int32.Parse(split[1]);
		maxLevelPayment = Int32.Parse(split[2]);

		// FIXME Sometimes there are extra strings when selecting level payment
		paymentPromptText.text = "Payment Range: " + minLevelPayment.ToString() + " - " + maxLevelPayment.ToString();
	}

	public void StartLevel() {
		string paymentInputText = paymentInputField.text;
		if (paymentInputText != string.Empty) {
			// Check if paymentOffer is valid
			try {
				int paymentOffer = Int32.Parse(paymentInputText);
				if ((paymentOffer >= minLevelPayment) && (paymentOffer <= maxLevelPayment)) {
					if (paymentOffer <= GlobalControl.Instance.savedData.SafeDonutCount) {
						paymentPanel.SetActive(false);
						LevelManager.levelPayment = paymentOffer;
						// ***** The player doesn't get the level payment back *********
						GlobalControl.Instance.savedData.SafeDonutCount -= paymentOffer;
						// **************************************************************
						SceneManager.LoadScene(levelName);
						LevelManager.levelManager.UnpauseGame();
						LevelManager.levelManager.ActivateButtons();
					} else {
						paymentInputField.text = string.Empty;
						placeholder.GetComponent<Text>().text = "Not enough donuts...";
					}
				} else {
					paymentInputField.text = string.Empty;
					placeholder.GetComponent<Text>().text = "Enter in range...";
				}
			} catch (FormatException e) {
				Debug.Log("Formatting error: " + e);
			}
		}
		// Save the game so that the payment is removed even if the level isn't completed
		GlobalControl.Instance.Save();
	}

	public void CancelLevelSelection() {
		paymentPanel.SetActive(false);
		LevelManager.levelManager.ActivateButtons();
		LevelManager.levelManager.UnpauseGame();
	}
}
