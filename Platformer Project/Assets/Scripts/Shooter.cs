using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public bool isCoolingDown;
    [SerializeField] private GameObject orb;

    [SerializeField] private Transform castPointLeft;
    [SerializeField] private Transform castPointRight;

    [SerializeField] private float castImpulse;
    [SerializeField] private float castAmplitude;

    [SerializeField] private AnimationHandler anim;

    [SerializeField] private float coolDownTime;
    private float startTime;

    private Rigidbody2D rb;
    private PlayerMotion player;
    

    
    void Start()
    {
        
        isCoolingDown = false;
        startTime = 0;
        player = GetComponent<PlayerMotion>();
    }
    
    void Update()
    {
        if (isCoolingDown && (Time.time - startTime >= coolDownTime))
        {
            isCoolingDown = false;
        }
    }

    public void SetStartTime()
    {
        startTime = Time.time;
    }

    public void SpawnOrb()
    {
        bool cond = anim.isFacingRight;
        GameObject currentOrb = null;
        if (cond)
        {
            currentOrb = Instantiate(orb, castPointRight.position, Quaternion.identity);
        }
        else
        {
            currentOrb = Instantiate(orb, castPointLeft.position, Quaternion.identity);
        }
        if (currentOrb != null)
        {
            PlayerProjectileController controller = currentOrb.GetComponent<PlayerProjectileController>();
            controller.CallSetGraphics(cond);
            rb = currentOrb.GetComponent<Rigidbody2D>();

            if (anim.isFacingRight)
            {
                rb.velocity = new Vector2(castImpulse, castAmplitude);
            }
            else
            {
                rb.velocity = new Vector2(-1 * castImpulse, castAmplitude);
            }
        }
    }

}
    
