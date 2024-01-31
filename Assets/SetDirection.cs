using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : MonoBehaviour
{
    private bool allowAddition = true;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Direction")){
            collision.GetComponent<WallDetector>().isWall = true;
            if(allowAddition){
                allowAddition = false;
                collision.transform.parent.GetComponent<SpawnItem>().wallNum++;
                return;
            }
        }
    }
}
