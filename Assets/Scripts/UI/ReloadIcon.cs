using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadIcon : MonoBehaviour
{
    [SerializeField]
    private Character _character;

    [SerializeField]
    private Image _image;

    public void Update()
    {
        _image.fillAmount = Mathf.InverseLerp(0, _character.ReloadTime, _character.ReloadProgress);
    }
}