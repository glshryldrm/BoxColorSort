using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int index;
    Color color;
    public bool isClicked;
    public bool isClickedAfter;
    public bool isOrganized;
    public void SetColor(Color c, int i)
    {
        color = c;
        color.a = 1f;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = c;
        
        index = i;
    }
    
    public void CreateFX()
    {
        GameObject fx =  Instantiate(GameAssets.Instance.particlePrefab);
        fx.transform.position = transform.position;
        var mainModule = fx.GetComponent<ParticleSystem>().main;
        mainModule.startColor = color;
    }
    public bool IsCliked()
    {
        return isClicked;
    }
    public bool IsClickedAfter()
    {
       
        return isClickedAfter;
    }public bool IsOrganized()
    {
        return isOrganized;
    }
    public void ChangeAnimation()
    {
        if (index == 0)
        {
            isClicked = true;
            isClickedAfter = true;
        }
        else
        {
            isClicked = true;
            isClickedAfter = true;
            
        }
        Character[] chars = FindObjectsByType<Character>((FindObjectsSortMode)index);
        for (int i = 0; i < chars.Length; i++)
        {
            chars[i].isOrganized = true;
        }
    }
}
