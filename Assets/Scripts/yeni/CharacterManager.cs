using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    SoundManager soundManagerScript;
    private Camera cam;
    [SerializeField] LayerMask layer;
    [SerializeField] GameManager gameManager;
    

    void Start()
    {
        soundManagerScript = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        cam = Camera.main;
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
                
                Ray ray = cam.ScreenPointToRay(touch.position);
                RaycastHit hit;
               
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    if (hit.collider != null)
                    {
                        soundManagerScript.Jump();
                        gameManager.OrganizeCharacter(hit.collider.GetComponent<Character>());
                        hit.collider.GetComponent<Character>().ChangeAnimation();
                       
                    }
                }
            }
        }
    }

}
