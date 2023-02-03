using System.Collections.Generic;
using UnityEngine;

public class TunnelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject firstTunnelSectionPrefab;
    [SerializeField] private GameObject[] tunnelSectionPrefabs;
    [SerializeField] private float spawnDistance = 35.9f; 
    [SerializeField] private Transform rocketTransform; 

    private const float spawnDistanceGap = 35.9f;
    private Queue<GameObject> tunnelSections;
    private Vector3 spawnPosition;

    private void Start()
    {
        tunnelSections = new Queue<GameObject>();
        spawnPosition = new Vector3(0f,spawnDistance,0f);
        
        SpawnTunnelSection();
    }

    private void Update()
    {
        if (Vector3.Distance(rocketTransform.position, spawnPosition) < spawnDistance + spawnDistanceGap)
        {
            SpawnTunnelSection();
        }
    }

    private void SpawnTunnelSection()
    {
        GameObject tunnelSection;
        if (tunnelSections.Count > 3)
        {
            tunnelSection = tunnelSections.Dequeue();
            if(firstTunnelSectionPrefab.activeInHierarchy) firstTunnelSectionPrefab.SetActive(false);
        }
        else
        {
            tunnelSection = Instantiate(tunnelSectionPrefabs[Random.Range(0,tunnelSectionPrefabs.Length)]);
        }

        tunnelSection.transform.position = spawnPosition;
        tunnelSection.SetActive(true);
        tunnelSections.Enqueue(tunnelSection);
        spawnPosition += Vector3.up * spawnDistance;
    }

    public void ResetProgress()
    {
        firstTunnelSectionPrefab.SetActive(true);
        foreach (var section in tunnelSections)
        {
            Destroy(section);
        }
        tunnelSections.Clear();
        spawnPosition = new Vector3(0f,spawnDistance,0f);
    }
}

