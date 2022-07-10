using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowCaw : MonoBehaviour
{
    [SerializeField] GameObject CAW;
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject panel;

    public void EnableCaw()
    {
        CAW.SetActive(true);
    }

    public void Mirror()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void enableEffects()
    {
        lightning.SetActive(true);
        panel.SetActive(true);
    }
}
