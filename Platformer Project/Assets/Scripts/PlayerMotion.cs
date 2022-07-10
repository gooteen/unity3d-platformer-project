using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    [Header("Jumping & running")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float impulse = 1;

    [Header("Conditions")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isGroundedSpecial = false;
    [SerializeField] private bool onTheSpecialPlatorm = false;
    [SerializeField] private bool isClipping = false;

    [SerializeField] private Transform groundColliderTransform;
    
    [Header("Collider offsets")]
    [SerializeField] private float jumpingOffset;
    //[SerializeField] private float clippingOffset;

    [Header("Layer masks")]
    [SerializeField] private LayerMask jumpableSurfacesMask;
    //[SerializeField] private LayerMask blockSurfacesMask;
    [SerializeField] private LayerMask specialSurfacesMask;

    private Rigidbody2D rb;
    [SerializeField] private AnimationHandler anim;
    //[SerializeField] private AnimationCurve curve;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Move(float direction)
    {
        if (!isClipping || (isClipping && isGrounded))
        {
            rb.velocity = new Vector2(speed*direction, rb.velocity.y);   
        }
    }


    private void FixedUpdate()
    {
        Vector3 feetOverlap = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(feetOverlap, jumpingOffset, jumpableSurfacesMask);
        anim.Land(isGrounded);
        //isClipping = Physics2D.OverlapCircle(feetOverlap, clippingOffset, blockSurfacesMask);
        isGroundedSpecial = Physics2D.OverlapCircle(feetOverlap, jumpingOffset, specialSurfacesMask);
        anim.ManageJump(rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, impulse);
        }
        if (isGroundedSpecial && onTheSpecialPlatorm) 
        {
            rb.velocity = new Vector2(rb.velocity.x, impulse);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "platformTrigger")
        {
            onTheSpecialPlatorm = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "platformTrigger")
        {
            onTheSpecialPlatorm = false;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Horizontal_BLOCK" || col.gameObject.tag == "SoulBlock")
        {
            isClipping = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Horizontal_BLOCK" || col.gameObject.tag == "SoulBlock")
        {
            isClipping = false;
        }
    }
}
