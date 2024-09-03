using Characters;
using Characters.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private BasicHealth _damageable;

    [SerializeField]
    private Slider _slider;

    public void Awake()
    {
        _damageable.HealthChanged.AddListener(SetValueOnHealthChanged);
    }

    public void OnDisable()
    {
        _damageable.HealthChanged.RemoveListener(SetValueOnHealthChanged);
    }

    private void SetValueOnHealthChanged(int oldValue)
    {
        _slider.value = Mathf.InverseLerp(0, _damageable.MaxHealth, _damageable.HealthValue);
    }
}
