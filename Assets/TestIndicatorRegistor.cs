using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIndicatorRegistor : MonoBehaviour
{
    [Range(5,30)]
    [SerializeField] float destroyTimer = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Register", Random.Range(0,8));
    }

    // Update is called once per frame
    void Register()
    {
        if(IndicatorSystem.CheckIfObjectInSight(this.transform)){
            IndicatorSystem.CreateIndicator(this.transform);
        }
        Destroy(this.gameObject, destroyTimer);
    }
}
