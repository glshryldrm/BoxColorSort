using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int index;
    
    public void SetColor(Color color, int i)
    {
        
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
        var mainModule = particleSystem.main;
        mainModule.startColor = color;
        index = i;
    }
}
