using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private float tutorialDuration;
    [SerializeField] private GameObject[] windows;
    [SerializeField] private bool canBeShown;
    [SerializeField] private int counter;
    [SerializeField] private string tag;
    private GameObject currentWindow;
    private float startTime;

    void Start()
    {
        counter = 0;
        startTime = 0;
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
        canBeShown = false;
    }

    void Update()
    {
        Debug.Log("SAS " + (canBeShown && (Time.time - startTime >= tutorialDuration)));
        if(canBeShown && (Time.time - startTime >= tutorialDuration))
        {
            currentWindow.SetActive(false);
            canBeShown = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && counter == 0)
        {
            counter++;
            startTime = Time.time;
            for (int i = 0; i < windows.Length; i++)
            {
                if (windows[i].tag == tag)
                {
                    currentWindow = windows[i];
                    currentWindow.SetActive(true);
                } else
                {
                    windows[i].SetActive(false);
                }
                
            }
            canBeShown = true;
        }
    }
}
