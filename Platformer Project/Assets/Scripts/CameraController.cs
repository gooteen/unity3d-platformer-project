using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private Transform playerTransform;
    public float xOffset = 0;
    public float yOffset = 0;
    public float zOffset = 0;

    void Start()
    {
        if (playerTransform != null)
        transform.position = playerTransform.position + new Vector3(xOffset, yOffset, zOffset);
    }

    void Update()
    {
        if (playerTransform != null)
        transform.position = playerTransform.position + new Vector3(xOffset, yOffset, zOffset);
    }
}
