using Characters;
using Databases;
using Databases.Serializables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceLoader : MonoBehaviour
{
    [SerializeField]
    private Fight _fight;

    [SerializeField]
    private TextMeshProUGUI _experienceText;

    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private Statistic[] _everyLevelBuffs;

    [SerializeField]
    private Character _character;

    private ExperienceSerializable _experienceSerializable;

    private int _requiredExperiencesForNextLevel;

    private int _level;

    private int _experience;

    public void Start()
    {
        _experience = JsonDatabase.Instance.Experience.experience;

        _level = JsonDatabase.Instance.Experience.level;
        _experience = JsonDatabase.Instance.Experience.experience;
        _requiredExperiencesForNextLevel = JsonDatabase.Instance.Experience.requiredExperince;

        AddExperienceOnFightEnded();

        Fight.Ended.AddListener(AddExperienceOnFightEnded);

        for (var i = 0;i < _level;i++)
        {
            _character.AddStatistics(_everyLevelBuffs);
        }
    }

    public void OnApplicationQuit()
    {
        JsonDatabase.Instance.SaveExperience(new ExperienceSerializable()
        {
            experience = _experience,
            level = _level,
            requiredExperince = _requiredExperiencesForNextLevel
        });
    }

    public void OnDisable()
    {
        OnApplicationQuit();

        Fight.Ended.RemoveListener(AddExperienceOnFightEnded);
    }

    private void AddExperienceOnFightEnded()
    {
        if (_fight.CurrentEnemy != null)
        {
            _experience += _fight.CurrentEnemy.EnemyStatistics.Experience;
        }

        if (_requiredExperiencesForNextLevel < _experience)
        {
            _requiredExperiencesForNextLevel = _experience * 2;

            _level++;
        }

        _experienceText.text = _experience.ToString();
        _levelText.text = _level.ToString();

        _character.AddStatistics(_everyLevelBuffs);
    }
}
