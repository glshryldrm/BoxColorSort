using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManagerr : MonoBehaviour
{

    public static bool check = false;
    public TextMeshProUGUI levelText;
    public GameObject completeLevelUI;
    public GameObject reloadLevelUI;

    private void Start()
    {
        //changeLevelName();
        changeName();
        
    }
    public void ReloadLevel()
    {
        reloadLevelUI.SetActive(true);
        Invoke(nameof(ReloadLevelPrivate), 1f);
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
    public void ReloadLevelPrivate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
