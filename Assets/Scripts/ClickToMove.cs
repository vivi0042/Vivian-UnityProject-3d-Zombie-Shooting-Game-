using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Check if ray hits the ground (NavMesh)
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, UnityEngine.AI.NavMesh.AllAreas))
            {
                //Move agent to the clicked position
                navAgent.SetDestination(hit.point);
            }
        }
    }
}
