using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //speed set for Player
    public float speed;
    //A private component used in this class only
    private Rigidbody2D rb;
    //How fast the object is going
    private Vector2 moveVelocity;
    private void Start()
    {
        //Needs this type of Component to work
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //moving onto the grid both vertically and horizontal 
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //keep speed to record
        moveVelocity = moveInput.normalized * speed;
    }
    private void FixedUpdate()
    {
        //slows the character down
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
