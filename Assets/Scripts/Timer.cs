using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private int time = 60;
    [SerializeField] private bool destroyWhenExpire = true;
    [SerializeField] private UnityEvent unityEvent;

    private bool expired = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!expired)
        {
            time--;
            if(time < 0)
            {
                Debug.Log("Timer Expired");
                expired = true;
                unityEvent.Invoke();
                if(destroyWhenExpire)
                {
                    Destroy(this);
                }
            }
        }
    }
}
