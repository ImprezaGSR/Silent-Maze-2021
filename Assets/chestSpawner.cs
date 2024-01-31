using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform chestPrehab;
    public List<Transform> chestSpawners;
    public bool isChestRoom;
    private bool hasChestSpawn;
    private KeyLeft keyLeft;
    public bool isSpecialChestRoom = false;
    public GameObject target;

    // Update is called once per frame
    void Start(){
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();
    }
    void Update()
    {
        if(isChestRoom){
            if(target != null){
                target.SetActive(false);
            }
            transform.Find("Marker").GetComponent<SpriteRenderer>().color = Color.yellow;
            if(!hasChestSpawn){
                int range = Random.Range(0,chestSpawners.Count-1);
                if(isSpecialChestRoom){
                    chestPrehab.gameObject.SetActive(true);
                }else{
                    for(int i = 0; i < chestSpawners.Count; i++){
                        Transform chest = Instantiate(chestPrehab, chestSpawners[i]);
                        if(i == range){
                            chest.GetComponent<OpenChest>().isEmpty = false;
                        }
                    }
                }
                keyLeft.keyNum++;
                keyLeft.keyRemain++;
                hasChestSpawn = true;
            }
        }
    }
}
