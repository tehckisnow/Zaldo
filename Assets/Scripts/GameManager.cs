using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        //singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
