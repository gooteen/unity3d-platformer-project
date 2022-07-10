using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTriggerHandler : MonoBehaviour
{
    [SerializeField] private MenuController mc;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            mc.SetMode();
        }
        
    }
}
