using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public List<GameObject> Trees;
    private float randX;
    private float randZ;
    // Start is called before the first frame update
    void Start()
    {
        randX = Random.Range(-transform.lossyScale.x, transform.lossyScale.x);
        randZ = Random.Range(-transform.lossyScale.z, transform.lossyScale.z);
        Vector3 randomPos = new Vector3(randX, transform.localPosition.y, randZ);
        int randTree = Random.Range(0, Trees.Count);
        GameObject tree = Instantiate(Trees[randTree], transform) as GameObject;
        tree.transform.localPosition = randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
