using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvent : MonoBehaviour
{
    public GameObject caseText;
    public GameObject titleText;
    private SettingsMenu settings;
    private PlayerMovement player;
    void Start()
    {
        settings = FindObjectOfType<SettingsMenu>().GetComponent<SettingsMenu>();
        player = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        settings.canSetting = false;
        player.isMovable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableCaseText(){
        caseText.SetActive(true);
    }

    public void EnableTitleText(){
        titleText.SetActive(true);
    }

    IEnumerator DisableTexts(){
        player.isMovable = true;
        settings.canSetting = true;
        caseText.GetComponent<TypeSentences>().StartDiscontinuous();
        titleText.GetComponent<TypeSentences>().StartDiscontinuous();
        yield return new WaitForSeconds(2f);
        transform.parent.gameObject.SetActive(false);
    }
}
