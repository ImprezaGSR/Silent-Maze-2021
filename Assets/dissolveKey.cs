using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolveKey : MonoBehaviour
{
    List<Material> materials = new List<Material>();
    private Material material;
    public bool isDissolving = false;
    private float lerp = 0;
    void Start()
    {
        var renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            materials.AddRange(renders[i].materials);
        }
        SetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolving){
            lerp += Time.deltaTime * 2;
            SetValue(lerp);
            if(lerp >= 1){
                SetValue(0);
                gameObject.SetActive(false);
            }
        }
    }

    public void SetValue(float value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_Dissolve", value);
        }
    }
    public void StartDissolve(){
        isDissolving = true;
    }
}
