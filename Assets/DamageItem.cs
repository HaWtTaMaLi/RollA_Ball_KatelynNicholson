using UnityEngine;
using UnityEngine.AI;

public class DamageItem : MonoBehaviour
{
    //on collision enter > item to enemy 
    //use nav mesh for tracking to chase enemy
    //when collision item is destroyed
    //causing damage to enemy once
    //get component enemy health 
    //get component damage amount
    //get tag " enemy"

    public Transform enemy;
    private NavMeshAgent navMeshAgent;
    private GameObject damageItem;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (enemy != null)
        {
            navMeshAgent.SetDestination(enemy.position);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            damageItem.SetActive(true);
        }
        else
        {
            damageItem.SetActive(false);
            //cooldown
        }
    }
}
