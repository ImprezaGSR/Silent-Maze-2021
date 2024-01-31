using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject settingUI;
    public bool isSettingOn = false;
    public bool canSetting = true;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    void Update(){
        if(canSetting){
            if(Input.GetKeyDown(KeyCode.Escape)){
                if(!isSettingOn){
                    isSettingOn = true;
                    settingUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    gameManager.Pause();
                    return;
                }
                if(isSettingOn){
                    isSettingOn = false;
                    gameManager.Resume();
                    settingUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    return;
                }
            }
        }
    }
    void Start(){
        if(isSettingOn){
            settingUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            gameManager.Pause();
        }
        if(!isSettingOn){
            settingUI.SetActive(false);
        }
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i = 0; i<resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetVolume(float volume){
        Debug.Log(volume);
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
    public void SetIsSettingOn(bool newIsSettingOn){
        isSettingOn = newIsSettingOn;
    }
}
