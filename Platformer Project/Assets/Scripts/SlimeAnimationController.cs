using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationController : MonoBehaviour
{
    public bool isDead;
    [SerializeField] private GameObject slime;
    [SerializeField] private SlimeMovementController sm;
    [SerializeField] private Health health;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private SoulprintDrop soulprints;
    [SerializeField] private int points;
    [SerializeField] private LevelController controller;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private CapsuleCollider2D col;

    
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelController>();
        isDead = false;
        
        anim = GetComponent<Animator>();
        col = slime.GetComponent<CapsuleCollider2D>();
        rb = slime.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (!health.isAlive)
        {
            healthBar.SetActive(false);
            anim.SetTrigger("isDead");
            
        }
    }

    public void callDestroy()
    {
        isDead = !isDead;
        health.Destroy();
        controller.AddPoints(points);
    }

    public void callDrop()
    {
        soulprints.Drop();
    }


    public void Squeeze()
    {
        anim.SetTrigger("isSqueezing");
    }

    public void CallJump()
    {
        sm.Jump();
    }

    public void SetStatic()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.simulated = false;
    }

    public void ManageJump(float magnitude)
    {
        anim.SetFloat("FlightMagnitude", magnitude);
    }

    public void Land(bool cond)
    {
        anim.SetBool("isLanded", cond);
    }


    public void CallDamageAnim()
    {
        anim.SetTrigger("isDamaged");
    }

    
    public void CallCoolDown()
    {
        Debug.Log("called");
        sm.CoolDown();
    }
    

}
