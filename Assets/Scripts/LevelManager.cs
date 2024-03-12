using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static bool check = false;
    public TextMeshProUGUI levelText;
    public GameObject completeLevelUI;
    public GameObject reloadLevelUI;
    string levelKey = "currentLevel";
    string levelNameKey = "LevelName";
    private void Start()
    {
        LoadLevel(PlayerPrefs.GetInt(levelKey, 0));
        changeName();
    }
    public void LoadLevel(int level)
    {
        
        if (!SceneManager.GetActiveScene().buildIndex.Equals(PlayerPrefs.GetInt(levelKey)))
        {
            if (level == SceneManager.sceneCountInBuildSettings -1)
            {
                int randomLevel = Random.Range(0, 35);
                SaveCurrentLevel(levelKey, randomLevel);
                SceneManager.LoadScene(randomLevel);
            }
            else
            {
                SceneManager.LoadScene(level);
            }
        }
        else
        {
            SaveCurrentLevel(levelKey, level);
        }


    }
    public void LoadNextLevel()
    {
        //completeLevelUI.SetActive(true);

        Invoke(nameof(LoadNextLevelPrivate), 1f);

    }

    void changeName()
    {
        if (check)
        {
            int i = (PlayerPrefs.GetInt(levelNameKey) + 1);
            SaveCurrentLevel(levelNameKey, i);
            levelText.text = "LEVEL " + i;
            check = false;
        }
        else
        {
            int i = (PlayerPrefs.GetInt(levelNameKey, 1));
            SaveCurrentLevel(levelNameKey, i);
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
        //reloadLevelUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SaveCurrentLevel(string Key, int level)
    {
        PlayerPrefs.SetInt(Key, level);
        PlayerPrefs.Save();
    }
}
