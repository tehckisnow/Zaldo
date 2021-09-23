using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string targetScene = "SampleScene";

    public void Activate()
    {
        SceneManager.LoadScene(targetScene);
    }
}
