using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera camToBe;

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
                cameras[i].GetComponent<CinemachineVirtualCamera>().enabled = false;
            }
            camToBe.enabled = true;
            camToBe.Priority = 1;
        }
        
    }
}
