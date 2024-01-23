using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveToArea : MonoBehaviour
{
    private Vector3 targetPosition;
    private LevelManager levelManager;
    private Rigidbody rigidbodyComponent;
    private GameObject selectedSphere;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        rigidbodyComponent = gameObject.GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (levelManager != null)
        {
            SelectSphere(gameObject);
        }
    }

    IEnumerator MoveToTargetPosition()
    {
        
        
        levelManager.isMoving = true;
        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPosition = selectedSphere.transform.position;

        while (elapsed < duration)
        {
            selectedSphere.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        selectedSphere.transform.position = targetPosition;
        levelManager.isMoving = false;

    }


    public void SelectSphere(GameObject sphere)
    {
        if (!levelManager.isMoving)
        {
            selectedSphere = sphere;
            targetPosition = levelManager.FindLeftmostEmptySpot();
            StartCoroutine(MoveToTargetPosition());
        }
    }
}