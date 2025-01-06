using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyAfterTime = 1f;

    void OnCollisionEnter(Collision collision)
    {

        // Modify by johnson need change the whole logic

        if (collision.gameObject.tag == "NormalChaseZombie" || collision.gameObject.tag == "SmallChaseZombie")
        {
            CreateBulletBloodEffect(collision);// zombie blood 
        }
        else
        {
            CreateBulletHoleEffect(collision); // bullet hole
        }

        Destroy(gameObject);
    }


    void Start()
    {
        Invoke("DestroyBullet", destroyAfterTime); // Call the DestroyBullet function after destroyAfterTime seconds
    }

    void DestroyBullet()
    {
        Destroy(gameObject); // Destroy the bullet
    }

    //add by johnson for bullethole
    void CreateBulletHoleEffect(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        GameObject hole = Instantiate(BulletEffectReference.Instance.bulletHoleEffect, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(collision.gameObject.transform);
    }

    void CreateBulletBloodEffect(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        GameObject blood = Instantiate(BulletEffectReference.Instance.bulletBloodEffect, contact.point, Quaternion.LookRotation(contact.normal));

        //blood.transform.SetParent(collision.gameObject.transform);
    }
}