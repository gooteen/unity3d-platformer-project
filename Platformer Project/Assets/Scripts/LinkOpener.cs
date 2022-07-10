using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkOpener : MonoBehaviour
{
    [SerializeField] private string path = "https://docs.google.com/forms/d/e/1FAIpQLSf4Fn37c1jz89b4Jkg0luX3s_IPnACtbBo99daEfV3xNVFg1w/viewform?usp=sf_link";
    public void OpenLink()
    {
        Application.OpenURL(path);
    }
}
