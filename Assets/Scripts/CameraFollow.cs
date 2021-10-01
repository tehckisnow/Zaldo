using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform targetTransform = null;
    private Transform thisTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = GameManager.instance.player.transform;
        thisTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindCamera();
        FindTarget(); //finds new player instance on scene reload
        FollowTarget();
        
    }

    private void FindCamera()
    {
        if(GameManager.instance.currentCamera == null)
        {
            GameManager.instance.currentCamera = gameObject;
        }
    }

    //finds new player instance on scene reload
    private void FindTarget()
    {
        if(GameManager.instance.player != null)
        {
            targetTransform = GameManager.instance.player.transform;
        }
    }

    private void FollowTarget()
    {
        if(targetTransform != null)
        {
            Vector3 newPos = new Vector3(targetTransform.position.x, targetTransform.position.y, thisTransform.position.z);
            thisTransform.position = newPos;
        }
    }
}
