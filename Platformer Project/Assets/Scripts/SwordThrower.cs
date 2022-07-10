using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrower : MonoBehaviour
{
    [SerializeField] private GameObject sword;

    [SerializeField] private Transform throwPointLeft;
    [SerializeField] private Transform throwPointRight;

    [SerializeField] private float throwImpulse;
    [SerializeField] private float throwAmplitude;

    [SerializeField] private SkeletonAnimationController anim;

    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    private float seconds;
    private float startTime;
    
    private Rigidbody2D rb;
    private SkeletonMovement skeleton;
    private System.Random random;
    

    void Start()
    {
        random = new System.Random();
        seconds = (float)random.Next(minTime, maxTime);
        startTime = 0;
        skeleton = GetComponent<SkeletonMovement>();
    }

    void Update()
    {
      if (skeleton.canShoot && (Time.time - startTime >= seconds))
        {
            Debug.Log(seconds);
            Throw();
            random = new System.Random();
            startTime = Time.time;
            seconds = (float)random.Next(minTime, maxTime);
        }
    }

    public void SpawnSword()
    {
        bool cond = anim.isFacingLeft;
        GameObject currentSword = null;
        if (cond)
        {
            currentSword = Instantiate(sword, throwPointLeft.position, Quaternion.identity);
        }
        else
        {
            currentSword = Instantiate(sword, throwPointRight.position, Quaternion.identity);
        }
        if (currentSword != null) 
        { 
        SkeletonProjectileController controller = currentSword.GetComponent<SkeletonProjectileController>();
        controller.CallSetGraphics(cond);
        rb = currentSword.GetComponent<Rigidbody2D>();

        if (anim.isFacingLeft)
        {
            rb.velocity = new Vector2(-1 *Mathf.Abs(skeleton.GetDistance())*throwImpulse, throwAmplitude);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Abs(skeleton.GetDistance())*throwImpulse, throwAmplitude);
        }
        }
    }

    
    public void Throw()
    {
        anim.Throw();
    }
}
