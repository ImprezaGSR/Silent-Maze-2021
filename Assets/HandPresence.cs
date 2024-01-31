using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerChracteristics;
    private InputDevice targetDevice;
    public List<GameObject> controllerPrehabs;
    public GameObject handModelPrehab;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void TryInitialize(){
        List<InputDevice> devices = new List<InputDevice>();
        
        InputDevices.GetDevicesWithCharacteristics(controllerChracteristics, devices);

        foreach(var item in devices){
            Debug.Log(item.name+item.characteristics);
        }
        if(devices.Count > 0){
            targetDevice = devices[0];
            GameObject prehab = controllerPrehabs.Find(controller => controller.name == targetDevice.name);
            if(prehab){
                spawnedController = Instantiate(prehab, transform);
            }else{
                Debug.LogError("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrehabs[0], transform);
            }
            spawnedHandModel = Instantiate(handModelPrehab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }
    void UpdateHandAnimation(){
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)){
            handAnimator.SetFloat("Trigger", triggerValue);
        }else{
            handAnimator.SetFloat("Trigger",0);
        }
        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)){
            handAnimator.SetFloat("Grip", gripValue);
        }else{
            handAnimator.SetFloat("Grip",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid){
            TryInitialize();
        }else{
            if(showController){
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }else{
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
