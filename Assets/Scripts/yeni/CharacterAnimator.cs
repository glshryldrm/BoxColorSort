using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private const string IS_CLICKED = "isClicked";
    private const string IS_CLICKED_AFTER = "isClickedAfter";
    private const string IS_ORGANIZED = "isOrganized";


    [SerializeField] private Character character;
    private Animator animator;
    void Awake()
    {
        animator = character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_CLICKED, character.IsCliked());
        animator.SetBool(IS_CLICKED_AFTER, character.IsClickedAfter());
        animator.SetBool(IS_ORGANIZED, character.IsOrganized());
    }
    
}
