using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveToCube : MonoBehaviour
{
    private GameObject cubePrefab;

    public void SetCubePrefab(GameObject prefab)
    {
        cubePrefab = prefab;
    }

    private void OnMouseDown()
    {
        if (cubePrefab != null)
        {
            // Küpün rengini belirle, bu renkteki küreleri taþý
            Color cubeColor = Random.ColorHSV();
            MoveSpheresToMatchingCube(cubeColor);
        }
    }

    private void MoveSpheresToMatchingCube(Color cubeColor)
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        foreach (GameObject sphere in spheres)
        {
            if (sphere.GetComponent<Renderer>().material.color == cubeColor)
            {
                sphere.transform.position = transform.position;
            }
        }
    }
}


