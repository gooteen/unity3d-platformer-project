using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    [SerializeField] private CutsceneController cutscene;

    public void StartTheFight()
    {
        cutscene.readyToStartTheFight = true;
    }

    public void DestroyPanel()
    {
        Destroy(gameObject);
    }
    
}
