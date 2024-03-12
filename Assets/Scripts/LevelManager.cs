using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using HomaGames.HomaBelly;
public class LevelManager : MonoBehaviour
{
    [SerializeField] bool loadLevelOnStart = false;
    [SerializeField] List<string> levels = new List<string>();
    public static bool check = false;
    public TextMeshProUGUI levelText;
    public GameObject completeLevelUI;
    public GameObject reloadLevelUI;
    string levelKey = "currentLevel";
    string levelNameKey = "LevelName";
    private void Start()
    {
        if (loadLevelOnStart)
            LoadLevel(PlayerPrefs.GetInt(levelKey, 0));
        else
        {
            if (!HomaBelly.Instance.IsInitialized)
            {
                // Listen event for initialization
                Events.onInitialized += OnHomaInit;
            }
            else
            {
                Analytics.LevelStarted(PlayerPrefs.GetInt(levelKey, 0));
            }
        }

        changeName();
    }

    void OnHomaInit()
    {
        Analytics.LevelStarted(PlayerPrefs.GetInt(levelKey, 0));
    }
    public void LoadLevel(int level)
    {
        SaveCurrentLevel(levelKey, level);

        if (level == levels.Count)
        {
            int randomLevel = Random.Range(2, levels.Count);

            SceneManager.LoadScene(levels[randomLevel]);
        }
        else
        {
            SceneManager.LoadScene(levels[level]);
        }

    }
    public void LoadNextLevel()
    {
        //completeLevelUI.SetActive(true);

        Analytics.LevelCompleted();

        Invoke(nameof(LoadNextLevelPrivate), 1f);
    }

    void changeName()
    {
        if (check)
        {
            int i = (PlayerPrefs.GetInt(levelNameKey) + 1);
            SaveCurrentLevel(levelNameKey, i);
            if (levelText != null)
                levelText.text = "LEVEL " + i;
            check = false;
        }
        else
        {
            int i = (PlayerPrefs.GetInt(levelNameKey, 1));
            SaveCurrentLevel(levelNameKey, i);
            if (levelText != null)
                levelText.text = "LEVEL " + i;
        }
    }
    void LoadNextLevelPrivate()
    {
        check = true;
        changeName();

        int currentLevel = PlayerPrefs.GetInt(levelKey, 0);
        int nextLevel = currentLevel + 1;

        SaveCurrentLevel(levelKey, nextLevel);

        LoadLevel(nextLevel);

    }
    public void ReloadLevel()
    {
        Analytics.LevelFailed("Restart");
        //reloadLevelUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SaveCurrentLevel(string Key, int level)
    {
        PlayerPrefs.SetInt(Key, level);
        PlayerPrefs.Save();
    }
}
