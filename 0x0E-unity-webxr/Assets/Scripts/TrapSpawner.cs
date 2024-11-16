using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject alley;
    public GameObject trap;
    public int trapCount = 100;
    private List<GameObject> spawnedItems = new List<GameObject>();
    private MeshRenderer renderer;

    void Start()
    {
        renderer = alley.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            Debug.LogError("No MeshRenderer found on the target object: " + alley.name);
        }
        Spawn();
    }

    [ContextMenu("Spawn Traps")]
    public void Spawn()
    {
        if (renderer == null)
            return;
        Bounds bounds = renderer.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;

        for (int i = 0; i < trapCount; i++)
            SpawnItemWithinBounds(minX, maxX, minZ, maxZ);
    }

    void SpawnItemWithinBounds(float minX, float maxX, float minZ, float maxZ)
    {
        float percent = (maxZ - minZ) * 10 / 100;
        float spawnX = Random.Range(minX , maxX);
        float spawnZ = Random.Range(minZ + percent, maxZ - percent * 3);

        float spawnY = alley.transform.position.y + 0.9f;

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        float minDistance = 1.5f; // Minimum distance to avoid overlap
        foreach (GameObject existingTrap in spawnedItems)
        {
            if (existingTrap != null) // Ensure the trap still exists
            {
                float distance = Vector3.Distance(spawnPosition, existingTrap.transform.position);
                if (distance < minDistance)
                    return;
            }
        }

        GameObject newTrap = Instantiate(trap, spawnPosition, Quaternion.identity);

        spawnedItems.Add(newTrap);
    }

    public void DeleteAllSpawnedItems()
    {
        foreach (GameObject item in spawnedItems)
            if (item != null)
                Destroy(item);

        spawnedItems.Clear();
    }
}
