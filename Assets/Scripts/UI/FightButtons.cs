using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtons : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _playerCharacter;

    [SerializeField]
    private GameObject _fightButton;

    [SerializeField]
    private GameObject _escapeButton;

    public void Awake()
    {
        Fight.Started.AddListener(ToggleButtonsActiveOnFightEvent);
        Fight.Ended.AddListener(ToggleButtonsActiveOnFightEvent);
        Fight.Escaped.AddListener(ToggleButtonsActiveOnFightEvent);
    }

    public void OnDestroy()
    {
        Fight.Started.RemoveListener(ToggleButtonsActiveOnFightEvent);
        Fight.Ended.RemoveListener(ToggleButtonsActiveOnFightEvent);
        Fight.Escaped.RemoveListener(ToggleButtonsActiveOnFightEvent);
    }

    public void ToggleButtonsActiveOnFightEvent()
    {
        _fightButton.SetActive(!_fightButton.activeSelf);
        _escapeButton.SetActive(!_escapeButton.activeSelf);
    }
}
