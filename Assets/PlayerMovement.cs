using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float runSpeed = 15f;
    public float staminaMax = 100;
    Vector3 velocity;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    bool chestAlreadyHit = false;
    public bool isMovable = true;
    public bool autoMoving = false;
    public bool isHiding = false;
    public bool hasSeen = false;
    public bool hasCaught = false;
    private Transform autoTarget;
    private Transform originPos;
    private float lerpTime = 0;
    public float autoDuration = 1f;
    private string autoTargetType;
    public float unseenDistance = 30f;
    [SerializeField]
    private float stamina;
    private bool isRunning = false;
    private bool runAble = true;
    public Image staminaFill;
    public GameObject blackOut;
    private bool canJump = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        stamina = staminaMax;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        
        if(!isRunning){
            if(move == Vector3.zero){
                stamina += Time.deltaTime * 2;
            }
            if(move != Vector3.zero){
                stamina += Time.deltaTime;
            }
        }
        if(stamina >= staminaMax){
            stamina = staminaMax;
            runAble = true;
            var tempColor = staminaFill.color;
            tempColor.a = 1f;
            staminaFill.color = tempColor;
        }
        if(stamina <= 0){
            stamina = 0;
            runAble = false;
            var tempColor = staminaFill.color;
            tempColor.a = 0.25f;
            staminaFill.color = tempColor;
        }
        if(autoMoving = true && autoTarget != null){
            Debug.Log("Lerping");
            if(lerpTime < autoDuration){
                lerpTime += Time.deltaTime;
                float t = lerpTime/autoDuration;
                transform.position = Vector3.Lerp(originPos.position, autoTarget.position, t);
                transform.rotation = Quaternion.Lerp(originPos.rotation, autoTarget.rotation, t);
            }else{
                autoMoving = false;
                isMovable = true;
            }
            if(isHiding){
                if(autoTargetType == "Hide"){
                    if(FindObjectOfType<StalkerAI>() != null){
                        if(FindObjectOfType<StalkerAI>().detected && transform.Find("Presense").GetComponent<DistanceCheck>().GetDistance() < unseenDistance){
                            hasSeen = true;
                        }
                    }
                }
            }
        }
        if(isMovable){
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0){
                velocity.y = -2f;
            }
            // float x = Input.GetAxis("Horizontal");
            // float z = Input.GetAxis("Vertical");

            // Vector3 move = transform.right * x + transform.forward * z;

            if(Input.GetButton("Run") && move != Vector3.zero && runAble){
                isRunning = true;
                controller.Move(move * runSpeed * Time.deltaTime);
                stamina -= Time.deltaTime * 2;
            }else{
                isRunning = false;
                controller.Move(move * speed * Time.deltaTime);
            }
            
            if(canJump){
                if(Input.GetButtonDown("Jump") && isGrounded){
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }else{
            isRunning = false;
            // controller.Move(move * speed * Time.deltaTime);
        }
        
        if(chestAlreadyHit){
            chestAlreadyHit = false;
        }
        staminaFill.fillAmount = stamina / staminaMax;
        if(hasCaught){
            isMovable = false;
            isHiding = false;
            autoMoving = false;
            blackOut.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && !canJump){
            canJump = true;
            gameManager.SetDialogueBox("Jump Cheat On");
        }else if(Input.GetKeyDown(KeyCode.Alpha3) && canJump){
            canJump = false;
            gameManager.SetDialogueBox("Jump Cheat Off");
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.CompareTag("Catch") && !isHiding){
            Debug.Log("Collided");
            DesetAutoTarget();
            hasCaught = true;
            FindObjectOfType<StalkerAI>().GetComponent<StalkerAI>().stalkerDest = this.gameObject;
        }
    }

    public void SetAutoTarget(Transform target, string targetType){
        Debug.Log("Set");
        isMovable = false;
        StartCoroutine(AutoWait(0.4f, target, targetType));
    }
    public void DesetAutoTarget(){
        lerpTime = 0;
        Debug.Log("Deset");
        isMovable = true;
        autoMoving = false;
        autoTarget = null;
        Debug.Log(autoTarget);
    }
    
    IEnumerator AutoWait(float time, Transform target, string targetType){
        yield return new WaitForSeconds(time);
        autoMoving = true;
        autoTarget = target;
        originPos = transform;
        autoTargetType = targetType;
        Debug.Log(autoTarget);
        yield return new WaitForSeconds(1.25f);
        DesetAutoTarget();
    }

}
