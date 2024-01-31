using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject TitleUI;
    // Start is called before the first frame update
    public void DinSetActive(){
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void DinGSetActive(){
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void LogoGSetActive(){
        transform.GetChild(2).gameObject.SetActive(true);
    }   
    public void LogoSetActive(){
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void TitleUISetActive(){
        TitleUI.SetActive(true);
    }
    public void Deactivate(){
        StartCoroutine(WaitThenQuit());
    }
    IEnumerator WaitThenQuit(){
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(3).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
