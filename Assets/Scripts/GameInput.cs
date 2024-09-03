using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    public static UnityEvent BowChoosed { get; private set; } = new();

    public static UnityEvent SwordChoosed { get; private set; } = new();

    public static UnityEvent StartFight { get; private set; } = new();

    public static UnityEvent EscapeFromFight { get; private set; } = new();

    public static UnityEvent Heal {  get; private set; } = new();

    public static void OnBowChoosed()
    {
        BowChoosed.Invoke();
    }

    public static void OnSwordChoosed()
    {
        SwordChoosed.Invoke();
    }

    public static void OnStartFight()
    {
        StartFight.Invoke();
    }

    public static void OnEscapeFromFight()
    {
        EscapeFromFight.Invoke();
    }

    public static void OnHeal()
    {
        Heal.Invoke();
    }
}
