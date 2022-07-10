using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulprintCounter : MonoBehaviour
{
    //public bool isEmpty;
    //[SerializeField] private float totalSoulprints;
    [SerializeField] private float currentSoulprints;
    [SerializeField] private Image soulprintInd;
    [SerializeField] private Animator indAnim;
    [SerializeField] private float soulprintValue;
    [SerializeField] private float castCost;
    [SerializeField] private Animator effect;

    void Start()
    {
        currentSoulprints = 0;
        //isEmpty = true;
    }

    void Update()
    {
        if (GetComponent<SoulprintCounter>() != null)
        {
            if (currentSoulprints >= castCost)
            {
                soulprintInd.enabled = true;
                indAnim.SetTrigger("toCast_ready");
            }
            else
            {
                soulprintInd.enabled = false;
                indAnim.SetTrigger("toEmpty");
            }
        }
       
        /*
        if (currentSoulprints == 0)
        {
            isEmpty = true;
        } else
        {
            isEmpty = false;
        }
        */
    }

    public void AddSoulprint()
    {
        
            currentSoulprints += soulprintValue;
            
        effect.Play("Soulprint_pickup");
        //Debug.Log("point added");
    }

    public void SubSoulprint()
    {
        if (currentSoulprints <= castCost)
        {
            currentSoulprints = 0;
        } else
        {
            currentSoulprints -= castCost;
        }
            
    }

    public bool Check()
    {
        if (currentSoulprints >= castCost)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
