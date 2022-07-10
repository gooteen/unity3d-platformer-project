using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHalt : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine("Disappear");
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }

}
