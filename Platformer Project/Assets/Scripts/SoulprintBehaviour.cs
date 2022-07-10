using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulprintBehaviour : MonoBehaviour
{
    [SerializeField] private float timeUntilInteractible;
    private SoulprintCounter counter;
    private float startTime;
    private bool isInteractible;
    private CapsuleCollider2D trigger;

    public void Start()
    {
        trigger = GetComponent<CapsuleCollider2D>();
        //isInteractible = false;
        trigger.enabled = false;
        startTime = Time.time;
    }

    public void Update()
    {
        if (Time.time - startTime >= timeUntilInteractible)
        {
            //isInteractible = true;
            trigger.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" /*&& isInteractible*/)
        {
            counter = col.gameObject.GetComponent<SoulprintCounter>();
            counter.AddSoulprint();
            Destroy(gameObject);
        }
    }
}
