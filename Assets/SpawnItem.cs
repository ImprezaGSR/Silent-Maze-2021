using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private Transform chestPrehab = null;
    public int wallNum;
    private Transform thisTransform;
    private Transform direction;
    public int dirNum = 4;
    private bool hasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        Debug.Log(thisTransform);
        StartCoroutine(waitForDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        DeleteDirs();
        if(transform.childCount < 4){
            // if(wallNum > 0){
            //     if(wallNum >= 3){
            //         if(!hasSpawned){
            //             hasSpawned = true;
            //             Debug.Log("Spawn!");
            //             var chest = Instantiate(chestPrehab, transform.position, transform.GetChild(0).rotation);
            //             Destroy(gameObject);
            //         }   
            //     }
            // }
            if(transform.childCount == 1 && !hasSpawned){
                hasSpawned = true;
                Debug.Log("Spawn!");
                var chest = Instantiate(chestPrehab, transform.position, transform.GetChild(0).rotation);
            }
            Destroy(gameObject);
        }
    }

    private void DeleteDirs(){
        if(transform.Find("Up") != null && transform.Find("Up").GetComponent<WallDetector>().isWall){
            Destroy(transform.Find("Up").gameObject);
        }
        if(transform.Find("Down") != null && transform.Find("Down").GetComponent<WallDetector>().isWall){
            Destroy(transform.Find("Down").gameObject);
        }
        if(transform.Find("Right") != null && transform.Find("Right").GetComponent<WallDetector>().isWall){
            Destroy(transform.Find("Right").gameObject);
        }
        if(transform.Find("Left") != null && transform.Find("Left").GetComponent<WallDetector>().isWall){
            Destroy(transform.Find("Left").gameObject);
        }
    }

    IEnumerator waitForDestroy(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
