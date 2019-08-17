using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class GlobalControl : MonoBehaviour {

    public static GlobalControl Instance;

    public SaveValues savedData; // Will save this whenever a change to settings is made. Won't have safeDonutCount updated in level
                                 // public SaveValues inLevelSaveData; // Will only save this at the end of a level so donuts collected aren't added until completion of level

    public HeadItem itemToModify; // TODO Probably should figure out a better way to pass this to the next scene

    void Awake() {
        if (Instance == null) {
            Debug.Log("Creating new GlobalControl");
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Debug.Log("musicVolume: " + savedData.musicVolume);
        } else if (Instance != this) {
            Debug.Log("Updating GlobalControl");
            Destroy(gameObject);
            Debug.Log("musicVolume: " + savedData.musicVolume);
        }
        Load();
    }

    public void Update() {
        // TODO Maybe check for escape button inside SceneLoader.cs
        if (Input.GetKey(KeyCode.Escape)) {
            //			if (!InLevelSettings.Settings.SettingsOpen) {
            if (SceneManager.GetActiveScene().name.Equals("MainMenu")) {
                // UNDONE Ask if user wants to exit app
                AskToQuit();
            } else if (SceneManager.GetActiveScene().name.Equals("LevelSelection")) {
                SceneManager.LoadScene("MainMenu");
            } else if (SceneManager.GetActiveScene().name.Equals("Customization")) {
                GlobalControl.Instance.Save(); // Saves the items placed on the pig
                SceneManager.LoadScene("LevelSelection");
            } else if (SceneManager.GetActiveScene().name.Equals("ItemCreation")) {
                SceneManager.LoadScene("Customization");
            } else {
                // UNDONE Ask if user wants to return to quit level
                SceneManager.LoadScene("LevelSelection");
            }
            //			} else {
            //				InLevelSettings.Settings.CloseSettings ();
            //			}
        }
    }

    void AskToQuit() {
        Application.Quit();
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveValues.sheep");
        bf.Serialize(file, savedData);
        file.Close();
        Debug.Log("Saved: " + savedData.SafeDonutCount);
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/saveValues.sheep")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveValues.sheep", FileMode.Open);
            savedData = (SaveValues)bf.Deserialize(file);
            file.Close();
            Debug.Log("musicMuted : " + savedData.musicMuted);
            Debug.Log("Loaded. Donuts: " + savedData.SafeDonutCount);
        } else {
            NewGame();
        }
    }

    /// <summary>
    ///  Initialize save values when starting a new game.
    /// </summary>
    private void NewGame() {
        savedData.SafeDonutCount = 10000;
        savedData.SafeClothCount = 1000;
        savedData.CompletedLevels = new bool[3] { true, true, false }; // First level is LevelSelection (level 0)
        savedData.musicMuted = false;
        savedData.musicVolume = 1f; // Full volume
        savedData.headItems = new List<HeadItem>();
        savedData.unlockedHeadItems = new List<HeadItem>();
        savedData.unlockedHeadItems.Add(new HeadItem("PartyHat", SerializableColor.white, new SpriteRenderer(), 5, 10, false));
        savedData.oneTimeItems = new List<OneTimeItem>();
        Debug.Log("New game");
    }
}
