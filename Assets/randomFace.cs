using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomFace : MonoBehaviour
{
    private float lerpTime = 0;
    private float targetZRotation;
    private Vector3 targetScale;
    private Transform prevTrans;
    private float randomTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        lerpTime = 0;
        prevTrans = transform;
        RandomRotation();
        RandomScale();
        StartCoroutine(RandomChild());
    }

    void Update(){
        lerpTime += Time.deltaTime*2;
        if(lerpTime >= 1){
            lerpTime = 0;
            prevTrans = transform;
            RandomRotation();
            RandomScale();
        }
        prevTrans.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(prevTrans.eulerAngles.z, targetZRotation, lerpTime));
        transform.eulerAngles = prevTrans.eulerAngles;
        prevTrans.localScale = Vector3.Lerp(prevTrans.localScale, targetScale, lerpTime);
        transform.localScale = prevTrans.localScale;

    }

    private void RandomRotation(){
        targetZRotation = Random.Range(-360f,360f);
    }
    private void RandomScale(){
        float targetScaX = Random.Range(0.3f,0.7f);
        float targetScaY = Random.Range(0.3f,0.7f);
        float targetScaZ = Random.Range(0.3f,0.7f);
        targetScale = new Vector3(targetScaX, targetScaY, targetScaZ);
    }
    IEnumerator RandomChild(){
        randomTime = Random.Range(0.5f,2f);
        yield return new WaitForSeconds(randomTime);
        int randNum = Random.Range(0,transform.childCount);
        for(int i = 0; i<transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
            if(i == randNum){
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        StartCoroutine(RandomChild());
    }
}
