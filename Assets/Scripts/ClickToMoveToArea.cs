using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveToArea : MonoBehaviour
{
    private Vector3 targetPosition;
    //private bool isFirstClick = true;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnMouseDown()
    {
        // Belirli bir alana yerleþtirilecek konumu ayarla
        int sphereIndex = levelManager.GetSphereIndex(gameObject);
        targetPosition = levelManager.GetCubePosition(sphereIndex);

        // Yalnýzca týklanan sphere prefabýný taþý
        MoveToTargetPosition();

    }
    private void MoveToTargetPosition()
    {

        transform.position = targetPosition;


    }

}