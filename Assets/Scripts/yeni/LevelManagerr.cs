using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManagerr : MonoBehaviour
{
    public static int level = 1;
    static int i = 1;
    public static bool check = false;
    public TextMeshProUGUI levelText;

    private void Start()
    {
        changeLevelName();
    }
    public void LoadLevel(int level)
    {

        SceneManager.LoadScene(LevelName());
        

    }
    public void ReloadLevel()
    {
        LoadLevel(level);


    }
    public void LoadNextLevel()
    {
        
        level++;
        LoadLevel(level);
        
    }
    string LevelName()
    {
        if (level == 5)
        {
            level = 1;
        }
        string levelName = "Level" + level; // Seviye adý, örneðin "Level1", "Level2" gibi
        return levelName;
    }
    void changeLevelName()
    {

        if (check)
        {
            i--;
            check = false;
            string levelNameText = "Level " + i;
            levelText.text = levelNameText;
            i++;

        }
        else
        {
            string levelNameText = "Level " + i;
            levelText.text = levelNameText;
            i++;

        }
            
            
    }
}
