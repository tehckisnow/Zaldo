﻿using System.Collections;
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
    public PersistentData persistentData;

    //public Dictionary<string, bool> gameData = new Dictionary<string, bool>();
    public Dictionary<string, Dictionary<string, bool>> gameData = new Dictionary<string, Dictionary<string, bool>>();

    // Start is called before the first frame update
    void Awake()
    {
        //singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }

    // //returns a bool indicating whether key was previously registered or not
    // public bool RegisterData(string idObject, string idValue, bool data)
    // {
    //     if(gameData.ContainsKey(idObject))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         if(gameData[idObject].ContainsKey(idValue))
    //         {
    //             gameData[idObject].Add(idValue, data);
    //         }
    //         return false;
    //     }
    // }

    // public void SetData(string idObject, string idValue, bool data)
    // {
    //     gameData[idObject][idValue] = data;
    // }

    // public bool GetData(string idObject, string idValue)
    // {
    //     return gameData[idObject][idValue];
    // }

}
