using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject animationUI;

    public void AnimationUISetActive(){
        animationUI.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
    
}
