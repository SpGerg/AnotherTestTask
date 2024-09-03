using Assets.Scripts.Characters.Enemies;
using Characters;
using Characters.Enemies;
using Factories;
using Inventories.Items;
using Inventories.Items.Enums;
using Scripts.Factories;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fight : MonoBehaviour
{
    public static UnityEvent Ended { get; } = new();

    public static UnityEvent Started { get; } = new();

    public static UnityEvent Escaped { get; } = new();

    public static bool IsInProgress { get; private set; }

    public Enemy CurrentEnemy => _currentEnemy;

    public Enemy LastEnemy => _lastEnemy;

    [SerializeField]
    private CharacterStatistics _characterStatistics;

    [SerializeField]
    private CharacterAndEnemyStatistics[] _enemies;

    [SerializeField]
    private ItemAndChance[] _items;

    [SerializeField]
    private CharacterFactory _characterFactory;

    [SerializeField]
    private ItemFactory _itemFactory;

    [SerializeField]
    private Transform _enemySpawn;

    [SerializeField]
    private RectTransform _lootSpawn;

    [SerializeField]
    private Transform _playerSpawn;

    [SerializeField]
    private int _delay;

    [SerializeField]
    private Character _player;

    private CharacterAndEnemyStatistics CurrentEnemyStatistics => _enemies[_index];

    private Enemy _currentEnemy;

    private Enemy _lastEnemy;

    private bool _isStarted;

    private int _index;

    public void Start()
    {
        GameInput.StartFight.AddListener(StartCoroutineOnFightStart);
        GameInput.EscapeFromFight.AddListener(EscapeFromFight);

        _player.Health.Died.AddListener(CallEventOnPlayerCharacterDied);
    }

    public void OnDisable()
    {
        GameInput.StartFight.RemoveListener(StartCoroutineOnFightStart);
        GameInput.EscapeFromFight.RemoveListener(EscapeFromFight);

        _player.Health.Died.RemoveListener(CallEventOnPlayerCharacterDied);
    }

    public void StartFight()
    {
        if (_isStarted || _player.CurrentWeapon == null)
        {
            return;
        }

        IsInProgress = true;

        _isStarted = true;

        var enemy = GetEnemy();

        _currentEnemy = _characterFactory.CreateEnemy(_enemySpawn, enemy.CharacterStatistics, enemy.EnemyStatistics);

        _player.AttackWhileTargetAlive(_currentEnemy.Health);
        _currentEnemy.AttackWhileTargetAlive(_player.Health);

        _currentEnemy.Health.Died.AddListener(EndFightOnEnemyDied);

        Started.Invoke();
    }

    public void EscapeFromFight()
    {
        Escaped.Invoke();

        IsInProgress = false;

        _currentEnemy.CurrentWeapon.Target = null;
        _currentEnemy.Health.Died.RemoveListener(EndFightOnEnemyDied);
        _currentEnemy.Health.Kill();

        if (_player.CurrentWeapon != null)
        {
            _player.CurrentWeapon.Target = null;
        }

        _isStarted = false;

        _currentEnemy = null;
    }

    private ItemType GetRandomDrop()
    {
        foreach (var randomItem in _items)
        {
            var chance = UnityEngine.Random.Range(1, 100);

            if (randomItem.Chance < chance)
            {
                continue;
            }

            return randomItem.Type;
        }

        return ItemType.None;
    }

    private CharacterAndEnemyStatistics GetEnemy()
    {
        while (true)
        {
            foreach (var characterAndEnemyStatistics in _enemies)
            {
                var chance = UnityEngine.Random.Range(1, 100);

                if (chance > characterAndEnemyStatistics.EnemyStatistics.Chance)
                {
                    continue;
                }

                return characterAndEnemyStatistics;
            }
        }
    }

    private void StartCoroutineOnFightStart()
    {
        StartCoroutine(SpawnEnemyWithDelayCoroutine());
    }

    private void CallEventOnPlayerCharacterDied()
    {
        Ended.Invoke();
    }

    private void EndFightOnEnemyDied()
    {
        Ended.Invoke();

        _isStarted = false;
        IsInProgress = false;

        _currentEnemy.Health.Died.RemoveListener(EndFightOnEnemyDied);

        var reward = GetRandomDrop();

        if (reward is not ItemType.None)
        {
            _itemFactory.CreateGround(reward, _lootSpawn.localPosition);
        }

        _currentEnemy = null;

        if (_enemies.Length - 1 < _index)
        {
            return;
        }

        StartCoroutine(SpawnEnemyWithDelayCoroutine());
    }

    private IEnumerator SpawnEnemyWithDelayCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        StartFight();
    }
}

[Serializable]
public class ItemAndChance
{
    public ItemType Type { get => _type; set => _type = value; }

    public float Chance { get => _chance; set => _chance = value; }

    [SerializeField]
    private ItemType _type;

    [SerializeField]
    private float _chance;
}