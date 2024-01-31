using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonsterIndicator indicatorPrehab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, MonsterIndicator> Indicators = new Dictionary<Transform, MonsterIndicator>();
    
    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable()
    {
        Debug.Log("OnEnabled");
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }
    private void OnDisable()
    {
        Debug.Log("OnDisabled");
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }
    void Create(Transform target){
        if(Indicators.ContainsKey(target)){
            Indicators[target].Restart();
            return;
        }
        MonsterIndicator newIndicator = Instantiate(indicatorPrehab, holder);
        newIndicator.Register(target, player, new Action(()=> {Indicators.Remove(target);}));
        Indicators.Add(target, newIndicator);
    }
    bool InSight(Transform t){
        Vector3 screenPoint = camera.WorldToViewportPoint(t.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
