using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathController : MonoBehaviour
{
    public bool isBreathing;
    public bool hitWhenBreathing;
    [SerializeField] private GameObject flame;

    [SerializeField] private Transform breathPointLeft;
    [SerializeField] private Transform breathPointRight;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private Transform hitboxPointLeft;
    [SerializeField] private Transform hitboxPointRight;

    [SerializeField] private BossAnimationController anim;
    [SerializeField] private BreathAnimationController breathAnim;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    [SerializeField] private float breathingTime;
    private float seconds;
    private float startTime;
    private float breathStartTime;

    private Rigidbody2D rb;
    private BossController boss;
    private System.Random random;
    

    void Start()
    {
        
        isBreathing = false;
        flame.SetActive(false);
        random = new System.Random();
        seconds = (float)random.Next(minTime, maxTime);
        startTime = 0;
        boss = GetComponent<BossController>();
    }

    void Update()
    {
        if(hitWhenBreathing)
        {
            
            //Debug.Log("HitWhenBreathing");
            breathAnim.Stop();
            isBreathing = !isBreathing;
            random = new System.Random();
            startTime = Time.time;
            seconds = (float)random.Next(minTime, maxTime);
            hitWhenBreathing = false;
        }
        if (isBreathing && (Time.time - breathStartTime >= breathingTime))
        {
            
            //Debug.Log("StoppedOnTimer");
            anim.Stop();
            //anim.flipLock();
            breathAnim.Stop();
            isBreathing = !isBreathing;
            random = new System.Random();
            startTime = Time.time;
            seconds = (float)random.Next(minTime, maxTime);
        }
        if (boss.canBreathe && (Time.time - startTime >= seconds) && !isBreathing)
        {
            
            // Debug.Log("InitiatedBreathing");
            Debug.Log(seconds);
            Breathe();
            
        }
    }

    public void SpawnFlame()
    {
        Debug.Log("Called!");
        bool cond = anim.isFacingLeft;
        /*
        if (cond)
        {
            FlipBreathToTheLeft();
        }
        else
        {
            FlipBreathToTheRight();
        }
        */
        flame.SetActive(true);
        breathAnim.JumpToStart();
    }

    public void FlipBreathToTheRight()
    {
        
        
            flame.transform.position = breathPointRight.position;
            hitbox.transform.position = hitboxPointRight.position;
            sprite.flipX = true;
        
    }
    public void FlipBreathToTheLeft()
    {
        
            flame.transform.position = breathPointLeft.position;
            hitbox.transform.position = hitboxPointLeft.position;
            sprite.flipX = false;
        
    }

    public void Breathe()
    {
        anim.Attack();
        //anim.flipLock();
        isBreathing = true;
        breathStartTime = Time.time;
    }
}
