using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Vector3 localVelocity;


    void Start()
    {
        localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody2D>().velocity);
    }    

    void Update()
    {
        localVelocity.x = 0;
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(localVelocity);
    }
}
