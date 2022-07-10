using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private Transform shootPointLeft;
    [SerializeField] private Transform shootPointRight;

    [SerializeField] private EyeAnimationController anim;

    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    private float seconds;
    private float startTime;

    private EyeMovementController eye;
    private System.Random random;


    void Start()
    {
        random = new System.Random();
        seconds = (float)random.Next(minTime, maxTime);
        startTime = 0;
        eye = GetComponent<EyeMovementController>();
    }

    void Update()
    {
        if (eye.canShoot && (Time.time - startTime >= seconds))
        {
            Debug.Log(seconds);
            Attack();
            random = new System.Random();
            startTime = Time.time;
            seconds = (float)random.Next(minTime, maxTime);
        }
    }

    public void SpawnProjectile()
    {
        Debug.Log("Calledd");
        bool cond = anim.isFacingRight;
        GameObject currentProjectile = null;
        if (cond)
        {
            currentProjectile = Instantiate(projectile, shootPointRight.position, Quaternion.identity);
        }
        else
        {
            currentProjectile = Instantiate(projectile, shootPointLeft.position, Quaternion.identity);
        }
        if (currentProjectile != null)
        {
            EyeProjectileController controller = currentProjectile.GetComponent<EyeProjectileController>();
            // SET GRAPHICS TURN ON TO FLIP controller.CallSetGraphics(cond);
        }
    }


    public void Attack()
    {
        anim.Attack();
    }
}
