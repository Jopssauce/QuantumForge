using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D characterController2D;
    public Character character;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0 ||
            Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetFloat("X", characterController2D.facingDirection.x);
            animator.SetFloat("Y", characterController2D.facingDirection.y);
        }
        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0 ||
                Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            animator.SetFloat("X2", characterController2D.facingDirection.x);
            animator.SetFloat("Y2", characterController2D.facingDirection.y);
            animator.SetBool("isMoving", false);
        }
    }
}
