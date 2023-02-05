using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TunnelSpawner : MonoBehaviour
{
    [SerializeField] 
    private GameConfig _gameConfig;
    
    private GameObject _firstTunnelSectionPrefab;
    private GameObject[] _tunnelSectionPrefabs;
    private float _tunnelSectionHeight;
    private float _spawnDistanceGap;
    private Queue<GameObject> _tunnelSections;
    private Vector3 _spawnPosition;

    private void Awake()
    {
        _firstTunnelSectionPrefab = _gameConfig.firstTunnelSection;
        _tunnelSectionPrefabs = _gameConfig.randomTunnelSections;
        _tunnelSectionHeight = _gameConfig.tunnelSectionHeight;
        _spawnDistanceGap = _gameConfig.spawnDistanceGap;
        _tunnelSections = new Queue<GameObject>();
        _spawnPosition = new Vector3(0f, _tunnelSectionHeight, 0f);
    }

    public void SpawnTunnelSection()
    {
        GameObject tunnelSection;
        if (_tunnelSections.Count > 3)
        {
            tunnelSection = _tunnelSections.Dequeue();
            if (_firstTunnelSectionPrefab.activeInHierarchy)
            {
                _firstTunnelSectionPrefab.SetActive(false);
            }
        }
        else
        {
            tunnelSection = Instantiate(_tunnelSectionPrefabs[Random.Range(0, _tunnelSectionPrefabs.Length)]);
        }

        tunnelSection.transform.position = _spawnPosition;
        tunnelSection.SetActive(true);
        _tunnelSections.Enqueue(tunnelSection);
        _spawnPosition += Vector3.up * _tunnelSectionHeight;
    }

    public void ResetProgress()
    {
        _firstTunnelSectionPrefab.SetActive(true);
        foreach (var section in _tunnelSections)
        {
            Destroy(section);
        }
        _tunnelSections.Clear();
        _spawnPosition = new Vector3(0f, _tunnelSectionHeight, 0f);
    }
}


