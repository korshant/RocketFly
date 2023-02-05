using System.Collections.Generic;
using UnityEngine;

namespace RocketFly.Scripts
{
    public class TunnelSpawner : MonoBehaviour
    {
        private GameObject _firstTunnelSectionPrefab;
        private GameObject[] _tunnelSectionPrefabs;
        private float _tunnelSectionHeight;
        private Queue<GameObject> _tunnelSections;
        private Vector3 _spawnPosition;
        private Transform _tunnelSectionsParent;

        public void Configure(GameConfig gameConfig)
        {
            _firstTunnelSectionPrefab = gameConfig.firstTunnelSection;
            _tunnelSectionPrefabs = gameConfig.randomTunnelSections;
            _tunnelSectionHeight = gameConfig.tunnelSectionHeight;
            _tunnelSections = new Queue<GameObject>();
            _spawnPosition = new Vector3(0f, 0f, 0f);
            _tunnelSectionsParent = new GameObject("Environment").transform;
        }

        public void SpawnTunnelSection()
        {
            GameObject tunnelSection;

            if (_tunnelSections.Count == 0)
            {
                tunnelSection = Instantiate(_firstTunnelSectionPrefab);
            }
            else if (_tunnelSections.Count > 3)
            {
                tunnelSection = _tunnelSections.Dequeue();
            }
            else
            {
                tunnelSection = Instantiate(_tunnelSectionPrefabs[Random.Range(1, _tunnelSectionPrefabs.Length)]);
            }

            tunnelSection.transform.SetParent(_tunnelSectionsParent);
            tunnelSection.transform.position = _spawnPosition;
        
            tunnelSection.SetActive(true);
            _tunnelSections.Enqueue(tunnelSection);
            _spawnPosition += Vector3.up * _tunnelSectionHeight;
        }

        public void Reset()
        {
            print("Tunnel spawner reset");
            foreach (var section in _tunnelSections)
            {
                Destroy(section);
            }
            _tunnelSections.Clear();
            _spawnPosition = new Vector3(0f, 0f, 0f);
            SpawnTunnelSection();
            SpawnTunnelSection();
        }
    }
}


