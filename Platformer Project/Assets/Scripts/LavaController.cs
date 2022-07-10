using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{

    [SerializeField] private float damageInterval;
    [SerializeField] private float damage;
    private float startTime;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != null)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<Health>().TakeDamage(damage);
                startTime = Time.time;
            }
        }
    }
    void  OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject != null)
        {
            if (col.gameObject.tag == "Player")
            {
                if (damageInterval <= (Time.time - startTime))
                {
                    Debug.Log("damageInterval " + (damageInterval <= (Time.time - startTime)));
                    col.gameObject.GetComponent<Health>().TakeDamage(damage);
                    startTime = Time.time;
                }
            }
        }

    }
}
