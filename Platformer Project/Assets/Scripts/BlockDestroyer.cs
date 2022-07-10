using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{

    [SerializeField] public GameObject block;
   public void CallDestroy()
    {
        Destroy(block);
    }
}
