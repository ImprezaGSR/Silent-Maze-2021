using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerAppear : MonoBehaviour
{
    public GameObject marker;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            marker.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider collision){
        if(collision.CompareTag("Player")){
            marker.SetActive(true);
        }
    }
}
