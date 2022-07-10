using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationHandler : MonoBehaviour
{
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject playerDeath;
    [SerializeField] private float deathSpawnOffset = 0;
    [SerializeField] private HitboxController hitbox;
    [SerializeField] private Health health;

    [SerializeField] private Shooter shooter;

    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private bool isSlashing;
    [SerializeField] private bool isCasting;
    public bool isFacingRight;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("s");
        }
        sprite = GetComponent<SpriteRenderer>();
        isFacingRight = true;
        isSlashing = false;
        firePoint.localPosition = new Vector3(0.1350002f, 0f, 0f);
    }

    void Update()
    {
        if (!health.isAlive)
        {
            GameObject currentPlayerDeath = Instantiate(playerDeath, new Vector3(transform.position.x, transform.position.y + deathSpawnOffset, transform.position.z), Quaternion.identity);
            health.Destroy();
        }
    }

    public void Run(float magnitude)
    {
        if (magnitude != 0) 
        {
            //Debug.Log("Running");
            anim.SetBool("isRunning", true);
        } else
        {
            //Debug.Log("Standing");
            anim.SetBool("isRunning", false);
        }
    }

    public void Flip(float magnitude)
    {
        if (magnitude < 0 && isFacingRight)
        {
            //Debug.Log("Supposed to flip to the left");
            sprite.flipX = true;
            isFacingRight = !isFacingRight;
            firePoint.localPosition = new Vector3(-0.1350002f, 0f, 0f);
        } 
        if (magnitude > 0 && !isFacingRight)
        {
            //Debug.Log("Supposed to flip to the right");
            sprite.flipX = false;
            isFacingRight = !isFacingRight;
            firePoint.localPosition = new Vector3(0.1350002f, 0f, 0f);
        }
    }

    public void ManageJump(float magnitude)
    {
        anim.SetFloat("FlightMagnitude", magnitude);
    }

    public void Land(bool cond)
    {
        anim.SetBool("isLanded", cond);
    }

    public void ShowImpact()
    {
        anim.SetTrigger("isDamaged");
    }

    public void Slash()
    {
        anim.SetTrigger("SlashButtonPressed");
    }

    public void SetSlashTrue()
    {
        isSlashing = true;
    }

    public void SetSlashFalse()
    {
        isSlashing = false;
    }

    public void SetCastTrue()
    {
        Debug.Log("set true");
        isCasting = true;
    }

    public void SetCastFalse()
    {
        Debug.Log("set alse");
        isCasting = false;
    }

    public bool GetSlashing()
    {
        return isSlashing;
    }

    public bool GetCasting()
    {
        return isCasting;
    }

    public void ActivateSlashHitbox()
    {
        hitbox.Enable();
    }

    public void DeactivateSlashHitbox()
    {
        hitbox.Disable();
    }

    public void Cast()
    {
        anim.SetTrigger("CastButtonPressed");
    }

    public void CallSpawnOrb()
    {
        shooter.SpawnOrb();
    }



}
