using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Vector3 movement;

    public Transform Player;

    private Rigidbody rb;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        anim.SetFloat("Blend", movement.magnitude);
    }

    public void OnMovement(InputValue val)
    {
        movement = val.Get<Vector2>();
        movement.z = movement.y;
        movement.y = 0;
    }
    
    public void OnJump()
    {
        rb.velocity = new Vector3(movement.x, 6f, movement.z);
        //anim.SetTrigger("Jump");
    }

}
