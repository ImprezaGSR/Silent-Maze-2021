using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision){
        if(collision.CompareTag("Player") && !collision.GetComponent<PlayerMovement>().isHiding){
            Debug.Log("Speed = 1");
            transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 1;
        }
    }
}
