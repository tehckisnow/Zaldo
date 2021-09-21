using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{    
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    [SerializeField] private float chaseRadius = 3f;
    [SerializeField] private float waypointRadius = 1f;
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float chaseSpeed = 0.02f;

    private int currentWaypoint = 0;
    private Transform target = null;
    private Transform playerTransform;
    private bool ready = true;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.instance.player.gameObject.transform;
        target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
        ApproachTarget();
    }

    private void SetTarget()
    {
        //if player distance < chaseRadius, set as target
        if(Vector2.Distance(playerTransform.position, transform.position) < chaseRadius)
        {
            target = playerTransform;
        }
        //otherwise, set a waypoint as target
        else
        {
            if(waypoints.Count > 0)
            {
                if(Vector2.Distance(waypoints[currentWaypoint].position, transform.position) < waypointRadius)
                {
                    currentWaypoint++;
                    if(currentWaypoint > waypoints.Count - 1)
                    {
                        currentWaypoint = 0;
                    }
                }
                target = waypoints[currentWaypoint];
            }
            else
            {
                target = transform;
            }
        }
    }

    private void ApproachTarget()
    {
        //determine speed here
        float speed = moveSpeed;
        if(target == playerTransform)
        {
            speed = chaseSpeed;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
    }

    IEnumerator Wait(float time)
    {
        
        yield return new WaitForSeconds(time);
        ready = true;
    }
}
