using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator zombieAnimator;
    private Vector3 zombiePreviousPosition;

    private string bulletTag = "Bullet";
    private string playerTag = "Player";

    private string smallZombieTag = "SmallChaseZombie";
    private string normalZombieTag = "NormalChaseZombie";

    bool isIdle = false;

    private int getHitCount = 0;

    public GameObject attackRange; // for active the attack range collider

    // Start is called before the first frame update
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();                                     //get the animation component
        zombieAnimator.SetBool("isIdle", true);                                    //play the idle animation at start

        zombiePreviousPosition = transform.position;                                //set the current position to the variable zombiePreviousPosition
    }//end of Start()

    // Update is called once per frame
    void Update()
    {
        //walk animation when the time is more than 30
        if (Timer.remainingTime > 30)
        {
            if (IsMoving() == true)
            {
                zombieAnimator.SetBool("isWalking", true);
                zombieAnimator.SetBool("isIdle", false);
            }
            else if (IsMoving() == false)
            {
                zombieAnimator.SetBool("isIdle", true);
                zombieAnimator.SetBool("isWalking", false);
            }
        }
        //run animation when the time is less than 30
        else if(Timer.remainingTime <= 30)
        {
            if (IsMoving() == true)
            {
                zombieAnimator.SetBool("isWalking", false);
                zombieAnimator.SetBool("isRunning", true);
                zombieAnimator.SetBool("isIdle", false);
            }
            else if (IsMoving() == false)
            {
                zombieAnimator.SetBool("isWalking", false);
                zombieAnimator.SetBool("isIdle", true);
                zombieAnimator.SetBool("isRunning", false);
            }
        }

        //get hit animation
        if (gameObject.CompareTag(normalZombieTag))
        {
            if (getHitCount == 3)
            {
                zombieAnimator.SetBool("isDead", true);
                zombieAnimator.SetBool("isWalking", false);
                zombieAnimator.SetBool("isRunning", false);
                zombieAnimator.SetBool("isIdle", false);
            }
        }
        else if (gameObject.CompareTag(smallZombieTag))
        {
            if (getHitCount == 2)
            {
                zombieAnimator.SetBool("isDead", true);
                zombieAnimator.SetBool("isWalking", false);
                zombieAnimator.SetBool("isRunning", false);
                zombieAnimator.SetBool("isIdle", false);
            }
        }

        //set the position for check is moving or not
            zombiePreviousPosition = transform.position;                                //update the current position to the variable zombiePreviousPosition

    }//end of Update()

    void OnCollisionEnter(Collision other)
    {
        //if is bullet play gethit animation
        if (other.gameObject.CompareTag(bulletTag))
        {
            zombieAnimator.SetTrigger("getHit");
            getHitCount++;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //if stay is collision play attack animation
        if (other.gameObject.CompareTag(playerTag))
        {
            zombieAnimator.SetBool("isAttacking", true);
            zombieAnimator.SetBool("isWalking", false);
            zombieAnimator.SetBool("isIdle", false);

            attackRange.SetActive(true);
        }//end of if

    }//end of OnTriggerStay()

    void OnTriggerExit(Collider other)
    {
        attackRange.SetActive(false);
        zombieAnimator.SetBool("isAttacking", false);
        
    }

    bool IsMoving()
    {
        return zombiePreviousPosition != transform.position;                        //check the previous position and current position is the same or not
                                                                                    //if is same, means zombie not moving, return false
                                                                                    //if is not same, means zombie moving, true
    }//end of IsMoving()

    public int getGetHitCount()
    {
        return getHitCount;
    }
}
