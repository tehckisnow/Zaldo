using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public GameObject hud;
    public EffectLayer fadeEffect;
    public GameObject currentCamera;
    public Textbox textbox;

    public Dictionary<string, bool> gameData = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Awake()
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

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }

    //returns a bool indicating whether key was previously registered or not
    public bool RegisterData(string id, bool data)
    {
        if(gameData.ContainsKey(id))
        {
            return true;
        }
        else
        {
            gameData.Add(id, data);
            return false;
        }
    }

    public void SetData(string id, bool data)
    {
        gameData[id] = data;
    }

}
