using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class DamageHint : MonoBehaviour
{
    private Vector2 _position;

    private const float speed = 1;

    public void Awake()
    {
        _position = transform.position + new Vector3(0, 1);
    }

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _position, speed * Time.deltaTime);
    }
}
