using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchTracker : MonoBehaviour
{
    public delegate void RocketLaunched();
    public event RocketLaunched OnRocketLaunch;

    [Range(-13f, 0f)]
    [SerializeField]
    private float _startingYPos;

    private bool _isLaunched = false;

    public bool IsLaunched => _isLaunched;

    private void Update()
    {
        if (!_isLaunched && transform.position.y > _startingYPos)
        {
            _isLaunched = true;
            OnRocketLaunch?.Invoke();
        }
    }
}
