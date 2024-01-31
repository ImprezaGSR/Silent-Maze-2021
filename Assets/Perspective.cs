using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour
{
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision){
        if(collision.CompareTag("Player") && !player.isHiding){
            Debug.Log("It found you!");
            transform.parent.parent.GetComponent<StalkerAI>().detected = true;
        }
    }
}
