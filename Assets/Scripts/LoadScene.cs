using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string targetScene = "SampleScene";
    public GameObject defaultFocus = null;

    void Start()
    {
        if(defaultFocus != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultFocus, null);
        }
    }

    public void Activate()
    {
        SceneManager.LoadScene(targetScene);
    }
}
