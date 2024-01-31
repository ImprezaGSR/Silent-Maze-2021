using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeRoom : MonoBehaviour
{
    public bool isFirst = false;
    private bool isColliding = false;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Room")){
            if(isColliding){
                return;
            }
            isColliding = true;
            if(!isFirst){
                Debug.Log("Deleted "+gameObject.name+" at "+gameObject.transform.localPosition);
                FindObjectOfType<MazeRender>().GetComponent<MazeRender>().AddRoomNum();
                FindObjectOfType<MazeRender>().GetComponent<MazeRender>().AddOverridedRoom();
                Destroy(gameObject);
            }
            StartCoroutine(Reset());
        }
        if(collision.CompareTag("Wall")){
            if(isColliding){
                return;
            }
            isColliding = true;
            Debug.Log("Deleted "+gameObject.name+" at "+gameObject.transform.localPosition);
            FindObjectOfType<MazeRender>().GetComponent<MazeRender>().AddRoomNum();
            FindObjectOfType<MazeRender>().GetComponent<MazeRender>().AddOverridedRoom();
            Destroy(gameObject);
            StartCoroutine(Reset());
        }
        // isFirst = true;
    }
    void Start()
    {
        StartCoroutine(waitForFixedUpdate());
    }
    // void FixedUpdate(){

    // }

    IEnumerator waitForFixedUpdate(){
        yield return new WaitForFixedUpdate();
        isFirst = true;
    }
    // Update is called once per frame
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    } 
}
