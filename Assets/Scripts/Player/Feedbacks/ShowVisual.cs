using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class ShowVisual : MonoBehaviour
{
    [SerializeField] GameObject _view;

    private void Awake()
    {
        var healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeadUpdate += Refresh;
    }

    void Refresh(bool isDead) 
    {
        _view.SetActive(!isDead);
    }

}
