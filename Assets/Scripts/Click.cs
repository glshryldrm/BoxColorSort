using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public GameObject redSquare;
    public GameObject orangeSquare;
    public GameObject yellowSquare;

    private bool redClicked = false;
    private bool orangeClicked = false;

  
    private void OnMouseDown()
    {
        string sphereTag = gameObject.tag;

        if (!redClicked)
        {
            if (sphereTag == "red")
            {
                MoveToSquare(redSquare);
                redClicked = true;
            }
            else
            {
                Debug.LogError("Incorrect sphere tag: " + sphereTag);
            }
        }
        else if (redClicked && !orangeClicked)
        {

            if (sphereTag == "orange")
            {
                MoveToSquare(orangeSquare);
                orangeClicked = true;
            }
            else
            {
                Debug.LogError("Incorrect sphere tag: " + sphereTag);
            }
        }
        else if (redClicked && orangeClicked)
        {
            if (sphereTag == "yellow")
            {
                MoveToSquare(yellowSquare);
            }
            else
            {
                Debug.LogError("Incorrect sphere tag: " + sphereTag);
            }
        }
        
    }

    private void MoveToSquare(GameObject square)
    {
        transform.position = square.transform.position;
    }
}
