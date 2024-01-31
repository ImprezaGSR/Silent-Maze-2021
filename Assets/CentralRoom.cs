using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CentralRoom : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    public Transform cylinderHolder;
    private KeyLeft keyLeft;
    [SerializeField]
    private Transform keyHolePrehab = null;
    private bool isSpawned = false;
    void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>().GetComponent<NavMeshSurface>();
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();
        StartCoroutine(SpawnCylinders());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawned){
            if(keyLeft.keyNum > 0){
                if(keyLeft.keyNum >= 1){
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(true);
                }
                if(keyLeft.keyNum >= 2){
                    cylinderHolder.Find("2").gameObject.SetActive(true);
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(false);
                }
                if(keyLeft.keyNum >= 3){
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(true);
                }
                if(keyLeft.keyNum >= 4){
                    cylinderHolder.Find("4").gameObject.SetActive(true);
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(false);
                }
                if(keyLeft.keyNum >= 5){
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(true);
                }
                if(keyLeft.keyNum >= 6){
                    cylinderHolder.Find("6").gameObject.SetActive(true);
                    cylinderHolder.Find("1-3-5").gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator SpawnCylinders(){
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < cylinderHolder.childCount; i++){
            if(cylinderHolder.GetChild(i).gameObject.activeSelf){
                for(int j = 0; j < cylinderHolder.GetChild(i).childCount; j++){
                    Instantiate(keyHolePrehab, cylinderHolder.GetChild(i).GetChild(j));
                }
            }
        }
        navMeshSurface.BuildNavMesh();
        isSpawned = true;
    }
}
