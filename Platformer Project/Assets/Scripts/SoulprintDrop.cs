using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulprintDrop : MonoBehaviour
{
    
    [SerializeField] private GameObject soulprint;
    [SerializeField] private Transform dropSpot;

    [SerializeField] private int dropRate;
    [SerializeField] private float dropImpulse;

    [SerializeField] private float timeBetweenBursts;

    private Rigidbody2D rb;
    private System.Random random;
    private float startTime;
    [SerializeField] private bool loopBool;

    public void Start()
    {
        loopBool = false;
    }

    public void Update()
    {
     
    }
 
    public void Drop()
    {
        random = new System.Random();
        StartCoroutine(Cycle(dropRate));
        
    }
    public Vector2 GetDirection(int i)
    {
        int coef;


        if (i % 2 == 0) 
        {
            coef = 1;
        } else
        {
            coef = -1;
        }


        float x = (float)random.NextDouble() * coef;
        float y = (float)random.NextDouble();
        Vector2 direction = new Vector2(x, y).normalized;
        return direction;
    }

    public IEnumerator Cycle(int dropRate) 
    {
        for (int i = 0; i < dropRate; i++)
        {
            startTime = Time.time;
            loopBool = false;
            GameObject currentSoulprint = Instantiate(soulprint, dropSpot.position, Quaternion.identity);
            currentSoulprint.transform.position = new Vector3(currentSoulprint.transform.position.x, currentSoulprint.transform.position.y, 0f);
            Vector2 direction = GetDirection(i);
            Debug.Log(direction);

            rb = currentSoulprint.GetComponent<Rigidbody2D>();
            rb.velocity = direction * dropImpulse;

            yield return new WaitForSeconds(timeBetweenBursts);


        }
    }

}
