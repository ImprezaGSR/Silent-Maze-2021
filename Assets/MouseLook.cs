using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public float turnSpeed = 5f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!transform.parent.GetComponent<PlayerMovement>().hasCaught){
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }else{
            if(FindObjectOfType<StalkerAI>() != null){
                Vector3 direction = FindObjectOfType<StalkerAI>().transform.Find("Head").position - playerBody.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                playerBody.rotation = Quaternion.Lerp(playerBody.rotation, rotation, turnSpeed * Time.deltaTime);
            }
        }
    }
}
