using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseRange : MonoBehaviour
{
    public GameObject Player;
    private GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //parent = GetComponentInParent<ChasePlayer>();
    }//end of Start()

    // Update is called once per frame
    void Update()
    {

    }// end of Update()

    void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject == Player)
            {
                //Debug.Log("Player enter idle zombie range");
                gameObject.GetComponentInParent<ChasePlayer>().enabled = true;
            }//end of nested if
            

    }//end of OnTriggerEnter()
}
