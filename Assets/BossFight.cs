using UnityEngine;
using UnityEngine.AI;

public class BossFight : MonoBehaviour
{
    //attack logic 
    //
    public Transform player;
    private NavMeshAgent navMeshAgent;

    private float enemyHealth = 100;
    [SerializeField] public HealthBar enemyHealthBar;
    public GameObject damageItem;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }

        //if enemy dead = winTextObject.SetActive(true);
        //youWon = true;
    }
}
