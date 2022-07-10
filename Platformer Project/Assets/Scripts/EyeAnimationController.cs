using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimationController : MonoBehaviour
{
    public bool isFacingRight;
    public bool isDead;
    public bool isFalling;
    [SerializeField] private Health health;
    [SerializeField] private GameObject eye;
    [SerializeField] private GameObject healthBar;
    //[SerializeField] private SkeletonMovement m;
    [SerializeField] private SoulprintDrop soulprints;
    [SerializeField] private EyeShooter shooter;
    [SerializeField] private CircleCollider2D colExtra;
    [SerializeField] private BoxCollider2D damageCol;
    [SerializeField] private GameObject dealer;
    [SerializeField] private int points;
    [SerializeField] private LevelController controller;
    private SpriteRenderer sprite;
    private CapsuleCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;
    private bool flippable;
    

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelController>();
        isDead = false;
        isFalling = false;
        flippable = true;
        anim = GetComponent<Animator>();
        col = eye.GetComponent<CapsuleCollider2D>();
        rb = eye.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (isFacingRight)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    void Update()
    {
        if (!health.isAlive)
        {
            //healthBar.SetActive(false);
            removeCollider();
            isFalling = true;
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
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        col.enabled = false;
        dealer.SetActive(false);
        damageCol.enabled = false;
        colExtra.enabled = true;
        
    }

    public void Flip(bool playerOnTheRight)
    {
        
        if (flippable)
        {
            Debug.Log("isFacingRight: " + isFacingRight);
            Debug.Log("playerOnTheRight: " + playerOnTheRight);
            if (playerOnTheRight && !isFacingRight)
            {

                sprite.flipX = false;
                isFacingRight = !isFacingRight;
            }
            if (!playerOnTheRight && isFacingRight)
            {
                Debug.Log("sprite "+ (sprite == null));
                sprite.flipX = true;
                isFacingRight = !isFacingRight;
            }
        }
    }


    public void CallDamageAnim()
    {
        anim.SetTrigger("isDamaged");
    }

    public void SetTrigger()
    {
        anim.SetTrigger("Fallen");
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void CallSpawnProjectile()
    {
        shooter.SpawnProjectile();
    }

}
