using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overrideRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Room")){
            FindObjectOfType<MazeRender>().GetComponent<MazeRender>().deletedObjects.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}
