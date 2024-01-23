using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameManager gameManager;


    void Start()
    {
        camera = Camera.main;
        FindObjectOfType<Character>();
    }

    
    void Update()
    {
        
        MoveWithRay();
        
    }

    private void MoveWithRay()
    {
        if (Input.touchCount > 0)
        {
            // Ýlk dokunma olayýný al
           Touch touch = Input.GetTouch(0);

            // Dokunma baþladýysa
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    if(hit.collider != null)
                    {

                        gameManager.OrganizeCharacter(hit.collider.GetComponent<Character>());
                    }
                }
            }
        }
    }
}
