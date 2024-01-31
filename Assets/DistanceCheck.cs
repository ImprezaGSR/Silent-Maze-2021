using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceCheck : MonoBehaviour
{
    private bool sensed = false;
    private bool detected = false;
    public TextMeshProUGUI distanceBox;
    public GameManager gameManager;
    [SerializeField]
    private float distance;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<StalkerAI>() != null){
            detected = FindObjectOfType<StalkerAI>().GetComponent<StalkerAI>().detected;
        }
        if(sensed){
            Vector3 playerDistance = transform.parent.Find("Cylinder").position;
            Vector3 enemyDistance = GameObject.FindWithTag("Enemy").transform.Find("Cylinder").position;
            distance = Mathf.Sqrt(Mathf.Pow((playerDistance.x - enemyDistance.x),2) + Mathf.Pow((playerDistance.z - enemyDistance.z),2));
            distanceBox.text="Distance between The Monster: "+Mathf.RoundToInt(distance)+"m";
            if(playerMovement.hasCaught){
                if(distance < 1){
                    Debug.Log("Speed = 0");
                    FindObjectOfType<StalkerAI>().GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0;
                }
            }
        }else{
            distanceBox.text="";
        }

        // if(!detected){
        //     if(!sensed){
        //         distanceBox.text="";
        //     }
        //     if(sensed){
        //         Vector3 playerDistance = transform.parent.Find("Cylinder").position;
        //         Vector3 enemyDistance = GameObject.FindWithTag("Enemy").transform.Find("Cylinder").position;
        //         distance = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow((playerDistance.x - enemyDistance.x),2) + Mathf.Pow((playerDistance.z - enemyDistance.z),2)));
        //         distanceBox.text="Distance between The Monster: "+distance+"m";
        //         distance = 0;
        //     }
        // }else{
        //     distanceBox.text="It found you!";
        // }
        
    }
    private void OnTriggerStay(Collider collision){
        if(collision.CompareTag("Enemy")){
            sensed = true;
        }
    }
    private void OnTriggerExit(Collider collision){
        if(collision.CompareTag("Enemy")){
            sensed = false;
        }
    }
    // IEnumerator Epilepsy(){
    //     blackOut.SetActive(true);
    //     yield return new WaitForSeconds(startDistance / 20);
    //     blackOut.SetActive(false);
    // }
    // IEnumerator GameOver(){
    //     yield return new WaitForSeconds(1);
    //     gameManager.GameOver();
    // }

    public float GetDistance(){
        return distance;
    }
}
