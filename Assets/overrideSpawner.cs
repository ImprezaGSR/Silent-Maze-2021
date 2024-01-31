using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overrideSpawner : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Room")){
            Destroy(gameObject);
        }
    }
}
