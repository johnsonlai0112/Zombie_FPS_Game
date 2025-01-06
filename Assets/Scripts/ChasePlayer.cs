using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    public NavMeshAgent zombie;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            zombie.SetDestination(Player.transform.position);
        }
    
    }

    public void SetPlayer(GameObject Player)
    {
        this.Player = Player;
    }
}
