using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int index;
    public ParticleSystem particlePrefab;
    Color color;
    
    public void SetColor(Color c, int i)
    {
        color = c;
        color.a = 1f;
        gameObject.GetComponent<SkinnedMeshRenderer>().materials[0].color = c;
        
        index = i;
    }
    
    public void CreateFX()
    {
        ParticleSystem fx =  Instantiate(particlePrefab);
        fx.transform.position = transform.position;
        var mainModule = fx.main;
        mainModule.startColor = color;
    }
}
