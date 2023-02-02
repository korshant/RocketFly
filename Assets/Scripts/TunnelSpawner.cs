using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class TunnelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject firstTunnelSectionPrefab;
    [SerializeField] private GameObject tunnelSectionPrefab; // Tunnel section prefab
    [SerializeField] private float spawnDistance = 35.9f; // Distance at which a new tunnel section is spawned
    [SerializeField] private Transform rocketTransform; // Rocket transform reference

    private const float spawnDistanceGap = 35.9f;
    private Queue<GameObject> tunnelSections; // Object pool for tunnel sections
    private Vector3 spawnPosition; // Position to spawn new tunnel sections

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
            print("SPAWN");
            SpawnTunnelSection();
        }
    }

    // Function to spawn new tunnel sections
    private void SpawnTunnelSection()
    {
        // Use object pool technique to spawn new tunnel sections
        GameObject tunnelSection;
        if (tunnelSections.Count > 2)
        {
            tunnelSection = tunnelSections.Dequeue();
            if(firstTunnelSectionPrefab.activeInHierarchy) firstTunnelSectionPrefab.SetActive(false);
        }
        else
        {
            tunnelSection = Instantiate(tunnelSectionPrefab);
        }

        // Set the position of the new tunnel section
        tunnelSection.transform.position = spawnPosition;
        tunnelSection.SetActive(true);

        // Update the spawn position for the next tunnel section
        spawnPosition += Vector3.up * 35.9f;

        // Add the new tunnel section to the end of the queue
        tunnelSections.Enqueue(tunnelSection);
    }
}

