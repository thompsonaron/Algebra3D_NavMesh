using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private NavMeshAgent agent;
   // [SerializeField]
    private new Camera camera;

    public float accelerationConstant = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                agent.SetDestination(hit.point);
            }

            if (agent.hasPath)
            {
                // vector direction from player to target for movement
                Vector3 desiredVector = agent.steeringTarget - transform.position;
                float angle = Vector3.Angle(transform.forward, desiredVector);

                // will be fastger or slower depending on an angle -> fixes slow turning and overrunning
                agent.acceleration = agent.speed * angle * accelerationConstant;
            }
        }

      
    }

    private void OnDrawGizmos()
    {
        if (agent.hasPath)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(agent.steeringTarget, 2f);
        }
    }
}
