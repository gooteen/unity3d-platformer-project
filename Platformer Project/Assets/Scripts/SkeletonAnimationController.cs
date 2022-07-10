using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationController : MonoBehaviour
{
    public bool isFacingLeft;
    [SerializeField] private Health health;
    [SerializeField] private GameObject skeleton;
    [SerializeField] private GameObject healthBar;
    //[SerializeField] private SkeletonMovement m;
    [SerializeField] private SoulprintDrop soulprints;
    [SerializeField] private SwordThrower thrower;
    [SerializeField] private CapsuleCollider2D colExtra;
    [SerializeField] private BoxCollider2D damageCol;
    [SerializeField] private GameObject jumpArea;
    [SerializeField] private int points;
    [SerializeField] private LevelController controller;
    private SpriteRenderer sprite;
    private CapsuleCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;
    private bool flippable;
    public bool isDead;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelController>();
        isDead = false;
        flippable = true;
        anim = GetComponent<Animator>();
        col = skeleton.GetComponent<CapsuleCollider2D>();
        rb = skeleton.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (isFacingLeft)
        {
            sprite.flipX = true;
        } else
        {
            sprite.flipX = false;
        }
    }

    void Update()
    {
        if (!health.isAlive)
        {
            healthBar.SetActive(false);
            anim.SetBool("isDead", true);
            flippable = false;
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

    public void removeCollider()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        jumpArea.SetActive(false);
        //rb.bodyType = RigidbodyType2D.Static;
        col.enabled = false;
        damageCol.enabled = false;
        colExtra.enabled = true;
    }

    public void Flip(bool playerOnTheLeft)
    {
        if (flippable)
        {
            if (playerOnTheLeft && !isFacingLeft)
            {
                sprite.flipX = true;
                isFacingLeft = !isFacingLeft;
            }
            if (!playerOnTheLeft && isFacingLeft)
            {
                sprite.flipX = false;
                isFacingLeft = !isFacingLeft;
            }
        }
    }

    public void Walk(bool cond)
    {
        anim.SetBool("isWalking", cond);
    }


    public void CallDamageAnim()
    {
        anim.SetTrigger("isDamaged");
    }

    public void Throw()
    {
    anim.SetTrigger("Throw");
    }

    public void CallSpawnSword()
    {
    thrower.SpawnSword();
    }

}
