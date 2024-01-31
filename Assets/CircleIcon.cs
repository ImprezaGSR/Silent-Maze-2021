using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleIcon : MonoBehaviour
{
    [SerializeField]
    bool isSelected = false;
    [SerializeField]
    private float scaleNow = 1;
    [SerializeField]
    public float focusScale = 1.2f;
    [SerializeField]
    private float lerpAmount = 0;
    [SerializeField]
    private float colorLerpAmount = 0.25f;
    [SerializeField]
    private float transformLerpAmount = 0;
    [SerializeField]
    private Vector3 normalScale;
    [SerializeField]
    private Vector3 largeScale;
    [SerializeField]
    private bool isDeactivating = false;
    [SerializeField]
    private bool isMoving = false;
    [SerializeField]
    private Vector3 currentPos;
    [SerializeField]
    private Vector3 destination;

    void Start()
    {
        currentPos = transform.parent.GetComponent<RectTransform>().anchoredPosition3D;
        destination = currentPos;
    }
    void OnEnable(){
        currentPos = transform.parent.GetComponent<RectTransform>().anchoredPosition3D;
        normalScale = new Vector3(1,1,1);
        largeScale = new Vector3(focusScale, focusScale, focusScale);
    }
    public void textEnable(){
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
    void Update(){
        if(isSelected){
            lerpAmount += Time.deltaTime * 4;
            colorLerpAmount +=  Time.deltaTime * 3;
            if(lerpAmount > 1){
                lerpAmount = 1;
            }
            if(colorLerpAmount > 1){
                colorLerpAmount = 1;
            }
            Debug.Log(largeScale);
        }else{
            lerpAmount -= Time.deltaTime * 4;
            colorLerpAmount -=  Time.deltaTime * 3;
            if(lerpAmount < 0){
                lerpAmount = 0;
            }
            if(colorLerpAmount < 0.25f){
                colorLerpAmount = 0.25f;
            }
        }
        transform.localScale = Vector3.Lerp(normalScale, largeScale, lerpAmount);
        GetComponent<Image>().color = Color.Lerp(Color.HSVToRGB(0,0,0.25f),Color.HSVToRGB(0,0,1),colorLerpAmount);
        if(isMoving){
            transformLerpAmount += Time.deltaTime * 2;
            if(transformLerpAmount > 1){
                transformLerpAmount = 1;
            }
            Debug.Log(destination);
            transform.parent.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(currentPos, destination, transformLerpAmount);
        }else{
            transformLerpAmount -= Time.deltaTime * 2;
            if(transformLerpAmount < 0){
                transformLerpAmount = 0;
            }
            transform.parent.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(currentPos, destination, transformLerpAmount);
        }
    }
    public void SetIsSelected(bool newIsSelected){
        isSelected = newIsSelected;
    }
    public void SetIsMoving(bool newIsMoving){
        isMoving = newIsMoving;
        // transformLerpAmount = 1;
    }
    public void SetDestination(Vector3 dest){
        currentPos = transform.parent.GetComponent<RectTransform>().anchoredPosition3D;
        destination = dest;
    }
    public void Deactivate(){
        GetComponent<Animator>().SetBool("isShrinking",true);
        StartCoroutine(WaitForShrink());
    }
    IEnumerator WaitForShrink(){
        yield return new WaitForSeconds(0.25f);
        transform.parent.gameObject.SetActive(false);
    }
}
