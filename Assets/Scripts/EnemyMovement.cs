using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
        foreach (GameObject pickup in pickups)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), pickup.GetComponent<Collider>());
        }

        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
