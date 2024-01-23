using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int index;
    
    public void SetColor(Color color, int i)
    {
        
        gameObject.GetComponent<MeshRenderer>().material.color = color;
        index = i;
        Debug.Log(index);
    }
}
