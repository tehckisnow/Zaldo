using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    [SerializeField] private AudioSource sfx = null;

    public void Activate()
    {
        gameObject.SetActive(true);
        if(sfx != null)
        {
            sfx.Play();
        }
    }

    public void Deactivate()
    {
        if(sfx != null)
        {
            sfx.Play();
        }
        gameObject.SetActive(false);
    }
}
