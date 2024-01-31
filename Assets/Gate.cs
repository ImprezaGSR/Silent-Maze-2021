using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    private GameManager gameManager;
    private KeyLeft keyLeft;
    private bool open = false;
    public float duration = 1f;
    private float start = 0;
    private Vector3 originPos;
    public Vector3 changedPos;
    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.Find("GateModel").Find("Grid").localPosition;
        keyLeft = FindObjectOfType<KeyLeft>().GetComponent<KeyLeft>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(open){
            start += Time.deltaTime;
            float t = start/duration;
            transform.Find("GateModel").Find("Grid").localPosition = Vector3.Lerp(originPos, changedPos, t);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            if(keyLeft.keyNum - keyLeft.keyInserted > 0){
                gameManager.SetDialogueBox("Insert "+(keyLeft.keyNum - keyLeft.keyInserted)+" more keys to unlock the gate");
            }else{
                open = true;
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Interactive")){
            gameManager.SetDialogueBox("");
        }
    }
}
