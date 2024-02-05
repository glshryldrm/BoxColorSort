using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManagerr : MonoBehaviour
{

    public static bool check = false;
    public TextMeshProUGUI levelText;
    public GameObject completeLevelUI;

    private void Start()
    {
        //changeLevelName();
        changeName();
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextLevel()
    {
        completeLevelUI.SetActive(true);

        Invoke(nameof(LoadNextLevelPrivate), 1f);
        
    }
    
    void changeName()
    {
        if (check)
        {
            levelText.text = SceneManager.GetActiveScene().name;
            check = false;
        }
        else
        {
            levelText.text = SceneManager.GetActiveScene().name;
        }
    }
    void LoadNextLevelPrivate()
    {
        check = true;
        changeName();
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
