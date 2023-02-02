using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private TunnelSpawner _tunnelSpawner;
    [SerializeField] private Transform _rocketBaseTransform;
    [SerializeField] private RocketController _rocket;

    private void OnEnable()
    {
        _rocket.OnRocketHit += RocketOnOnRocketHit;
    }

    private void OnDisable()
    {
        _rocket.OnRocketHit -= RocketOnOnRocketHit;
    }

    private void RocketOnOnRocketHit()
    {
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                print("Show nice falling");
            })
            .AppendInterval(2f)
            .AppendCallback(StartGame);
    }

    public void StartGame()
    {
        _tunnelSpawner.ResetProgress();
        _rocket.gameObject.transform.position = _rocketBaseTransform.position;
        _rocket.gameObject.transform.rotation = _rocketBaseTransform.rotation;
    }
}
