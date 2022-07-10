using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] coeffs;
    private int layersSize;

    void Start()
    {
        layersSize = layers.Length;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < layersSize; i++)
        {
            float x = transform.position.x * coeffs[i];
            float y = transform.position.y * coeffs[i];
            float z;
            if (SceneManager.GetActiveScene().name != "Arena")
            {
                z = transform.position.z * coeffs[i];
            } else
            {
                z = layers[i].position.z;
            }
            layers[i].position = new Vector3(x, y, z); 
        }
    }
}
