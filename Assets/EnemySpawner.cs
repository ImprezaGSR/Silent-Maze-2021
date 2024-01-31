using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    // private NavMeshSurface navMeshSurface;
    private KeyLeft keyLeft;
    [SerializeField]
    private GameObject enemyPrehab = null;
    [SerializeField]
    private GameObject chakraPrehab = null;
    private bool spawned = false;
    private Transform enemySpawn;
    private int randNum = 0;
    void Start()
    {
        // navMeshSurface = FindObjectOfType<NavMeshSurface>().GetComponent<NavMeshSurface>();
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyLeft.keyRemain < keyLeft.keyNum && !spawned){
            while(!transform.GetChild(randNum).gameObject.CompareTag("Room") || transform.GetChild(randNum).GetComponent<chestSpawner>().isChestRoom){
                randNum = Random.Range(0, transform.childCount-1);
            }
            enemySpawn = transform.GetChild(randNum).Find("MonsterTarget").transform;
            // navMeshSurface.BuildNavMesh();
            spawned = true;
            Instantiate(enemyPrehab, enemySpawn.position, enemySpawn.rotation);
            Instantiate(chakraPrehab, enemySpawn.position, enemySpawn.rotation);
            Debug.Log("Spawned!");
            return;
        }
    }
}
