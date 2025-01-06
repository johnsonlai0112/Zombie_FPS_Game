using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffectReference : MonoBehaviour
{
    public static BulletEffectReference Instance { get; set; }

    public GameObject bulletHoleEffect;
    public GameObject bulletBloodEffect;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }

}
