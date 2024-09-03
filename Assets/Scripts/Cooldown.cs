using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Cooldown
{
    public Cooldown()
    {
        CooldownSeconds = 0;
    }

    public Cooldown(int seconds)
    {
        CooldownSeconds = seconds;
    }

    public bool IsReady => !_isUsed || RemainingTime <= Mathf.Epsilon;

    public float CooldownSeconds { get; set; }

    public float PassedTime => Time.time - _lastUsed;

    public float RemainingTime => Mathf.Max(0f, CooldownSeconds - PassedTime);

    private float _lastUsed;

    private bool _isUsed;

    public void ForceUse()
    {
        _lastUsed = Time.time;
        _isUsed = true;
    }
}