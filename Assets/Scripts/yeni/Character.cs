using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int index;
    public ParticleSystem particlePrefab;
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
        ParticleSystem fx =  Instantiate(particlePrefab);
        fx.transform.position = transform.position;
        var mainModule = fx.main;
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
}
