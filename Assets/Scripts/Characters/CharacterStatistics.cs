using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New character data", menuName = "Character data", order = 50)]
public class CharacterStatistics : ScriptableObject
{
    public int Armor { get => _armor; set => _armor = value; }

    public float AttackDelay { get => _attackDelay; set => _attackDelay = value; }

    public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public float AttackSpeed {
        get => _attackSpeed;
        set
        {
            var oldValue = _attackSpeed;

            _attackSpeed = value;

            AttackSpeedChanged.Invoke(oldValue);
        }
    }

    public RuntimeAnimatorController RuntimeAnimatorController { get => _runtimeAnimatorController; set => _runtimeAnimatorController = value; }

    public UnityEvent<float> AttackSpeedChanged { get; private set; } = new();

    [SerializeField]
    private RuntimeAnimatorController _runtimeAnimatorController;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _attackDelay;

    [SerializeField]
    private float _attackSpeed;

    [SerializeField]
    private int _armor;

    [SerializeField]
    private int _maxHealth;

    public CharacterStatistics Copy()
    {
        var result = CreateInstance<CharacterStatistics>();

        result.MaxHealth = MaxHealth;
        result.AttackDamage = AttackDamage;
        result.AttackDelay = AttackDelay;
        result.AttackDamage = AttackDamage;

        return result;
    }
}
