using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public bool isDead;
    public bool isFacingLeft;
    [SerializeField] private MenuController menu;
    [SerializeField] private Health health;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private BreathController breath;
    //[SerializeField] private BreathAnimationController breathAnim;
    [SerializeField] private GameObject hitbox;
    //[SerializeField] private SkeletonMovement m;
    [SerializeField] private SoulprintDrop soulprints;
    // [SerializeField] private SwordThrower thrower;
    [SerializeField] private int points;
    [SerializeField] private LevelController controller;
    private SpriteRenderer sprite;
    private CapsuleCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform hitboxPosition;
    [SerializeField] private bool flippable;
    

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<LevelController>();
        hitboxPosition = hitbox.transform;
        isDead = false;
        flippable = true;
        anim = GetComponent<Animator>();
        col = boss.GetComponent<CapsuleCollider2D>();
        rb = boss.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (isFacingLeft)
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
        if (hitbox != null)
        {
            hitbox.transform.position = hitboxPosition.position;
        }
        if (!health.isAlive)
        {
            isDead = true;
            healthBar.SetActive(false);
            anim.SetBool("isDead", true);
            flippable = false;
        }
    }

    public void CallSetMode()
    {
        menu.SetModeBoss();
    }

    public void SetX(float x)
    {
        if (!isFacingLeft)
        {
            x = x * -1;
        }
        hitboxPosition.localPosition = new Vector3(x, hitbox.transform.localPosition.y, 0f);
    }

    public void SetY(float y)
    {
       hitboxPosition.localPosition = new Vector3(hitbox.transform.localPosition.x, y, 0f);
    }


    public void callDestroy()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        Debug.Log("heyy" + spawners.Length);
        for (int i = 0; i < spawners.Length; i++)
        {
            Debug.Log(spawners[i]);
        }
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].GetComponent<SpawnController>().spawnable = false;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Damageable");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        controller.AddPoints(points);
    }

    public void callSpawnFlame()
    {
        
        breath.SpawnFlame();
    }

    
    public void callDrop()
    {
        soulprints.Drop();
    }
    

    public void removeCollider()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.bodyType = RigidbodyType2D.Static;
        //rb.bodyType = RigidbodyType2D.Static;
        col.enabled = false;
        
    }
    

    public void Flip(bool playerOnTheLeft)
    {
        if (flippable)
        {
            if (playerOnTheLeft && !isFacingLeft)
            {
                sprite.flipX = false;
                isFacingLeft = !isFacingLeft;
                breath.FlipBreathToTheLeft();
            }
            if (!playerOnTheLeft && isFacingLeft)
            {
                sprite.flipX = true;
                isFacingLeft = !isFacingLeft;
                breath.FlipBreathToTheRight();
            }
        }
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Stop()
    {
        anim.SetTrigger("Finished");
    }

    
    public void setFlippable(int cond)
    {
        if (cond == 1)
        {
            flippable = true;
        }
        else
        {
            flippable = false;
        }
        
    }
    

   
    public void CallDamageAnim()
    {
        anim.SetTrigger("Damaged");
        //breathAnim.RemoveCollider();
        if (breath.isBreathing)
        {
            breath.hitWhenBreathing = true;
        }
    }
   
}
