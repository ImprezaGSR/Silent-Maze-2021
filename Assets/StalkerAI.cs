using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class StalkerAI : MonoBehaviour
{
    public GameObject stalkerDest;
    NavMeshAgent stalkerAgent;
    public bool detected = false;
    private bool atDetectedSpeed = false;
    private int randNum;
    private TextMeshProUGUI distanceBox;
    public float defaultSpeed = 2;
    public float detectedSpeed = 4;
    private PlayerMovement player;
    private bool isLerping = false;
    private Transform originPos;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private float lerpTime = 0;
    private KeyLeft keyLeft;
    [Range(1f,2f)]
    public float maxMultiplier = 1.5f;
    private GameObject lastTarget;
    private GameObject[] targets;
    public GameObject the;
    public GameObject marker;
    private bool indicatorAppear = false;
    // Start is called before the first frame update
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
        keyLeft = FindObjectOfType<Canvas>().GetComponent<KeyLeft>();
        stalkerAgent = GetComponent<NavMeshAgent>();
        stalkerAgent.speed = defaultSpeed;
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        roamingTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLerping){
            lerpTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originPos.position, targetPos, lerpTime);
            transform.rotation = Quaternion.Lerp(originPos.rotation, targetRot, lerpTime);
            if(lerpTime >= 1){
                lerpTime = 0;
                isLerping = false;
            }
        }
        if(detected){
            if(!atDetectedSpeed){
                stalkerAgent.speed = detectedSpeed;
                atDetectedSpeed = true;
                the.SetActive(true);
            }
            stalkerDest = FindObjectOfType<PlayerMovement>().gameObject;
            Invoke("Register", 0);
        }
        if(!detected){
            atDetectedSpeed = false;
            the.SetActive(false);
        }
        if(stalkerDest != null){
            stalkerAgent.destination = stalkerDest.transform.position;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            marker.SetActive(true);
        }
    }

    private void roamingTarget(){
        stalkerAgent.speed = defaultSpeed;
        int randNum = Random.Range(0,targets.Length);
        // while(FindObjectOfType<autoBake>().transform.GetChild(randNum).Find("MonsterTarget") == null || !FindObjectOfType<autoBake>().transform.GetChild(randNum).Find("MonsterTarget").gameObject.activeSelf){
        //     if(FindObjectOfType<autoBake>().transform.GetChild(randNum).Find("MonsterTarget") == null || !FindObjectOfType<autoBake>().transform.GetChild(randNum).Find("MonsterTarget").gameObject.activeSelf){
        //         randNum = Random.Range(0,FindObjectOfType<autoBake>().transform.childCount-1);
        //     }
        // }
        while(!targets[randNum].activeSelf){
            randNum = Random.Range(0,targets.Length);
        }
        Debug.Log(randNum);
        stalkerDest = targets[randNum];
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Target")&&!detected){
            if(lastTarget != null){
                lastTarget.SetActive(true);
            }
            lastTarget = collision.gameObject;
            collision.gameObject.SetActive(false);
            roamingTarget();
        }
        if(collision.CompareTag("Outside")&&detected){
            stalkerAgent.speed = 0;
            originPos = transform;
            targetPos = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z);
            targetRot = collision.transform.rotation;
            isLerping = true;
            Debug.Log("Whether it's inside");
            StartCoroutine(WhetherInside(player.hasSeen, collision.transform.parent, player));
        }
        // if(collision.CompareTag("Player")){
        //     stalkerAgent.speed = 0;
        // }
    }

    void Register()
    {
        // if(IndicatorSystem.CheckIfObjectInSight(this.transform)){
        //     IndicatorSystem.CreateIndicator(this.transform);
        // }
        IndicatorSystem.CreateIndicator(this.transform);
    }
    IEnumerator WhetherInside(bool hasSeen, Transform locker, PlayerMovement player){
        yield return new WaitForSeconds(3);
        if(!hasSeen){
            detected = false;
            roamingTarget();
        }
        if(hasSeen){
            stalkerAgent.speed = 1;
            if(locker.GetComponent<locker>() != null){
                locker.GetComponent<locker>().isOpen = true;
            }
            yield return new WaitForSeconds(0.4f);
            player.DesetAutoTarget();
            player.isHiding = false;
            player.hasCaught = true;
        }
    }

    public void LevelUp(){
        float currentMultiplier = 1 + ((maxMultiplier-1) * ((float)((keyLeft.keyNum-1) - keyLeft.keyRemain)/(float)(keyLeft.keyNum-1)));
        Debug.Log(currentMultiplier);
        defaultSpeed *= currentMultiplier;
        detectedSpeed *= currentMultiplier;
        if(detected){
            stalkerAgent.speed = detectedSpeed;
        }else{
            stalkerAgent.speed = defaultSpeed;
        }
    }
    
}
