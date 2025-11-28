using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner3D : MonoBehaviour
{
    public Collider targetCollider;

    public LayerMask obstacleLayer;
    public LayerMask collectibleLayer;

    public int numberOfObstacles;
    public int numberOfCollectibles;

    public float distanceBetweenObstacles = 5f; 
    public float distanceBetweenCollectibles = 2f;

    public int padding;

    public List<GameObject> obstaclesToSpawn;
    public List<GameObject> collectiblesToSpawn;

    private void Start()
    {
        GenerateObjects(numberOfObstacles, distanceBetweenObstacles, obstaclesToSpawn, obstacleLayer, transform.localScale.y);
        GenerateObjects(numberOfCollectibles, distanceBetweenCollectibles, collectiblesToSpawn, collectibleLayer, transform.localScale.y + 0.5f);
    }

    public Vector3 GetRandomPointInBounds(Bounds bounds, float distanceBetweenObjects, LayerMask layer, float spawnHeight)
    {
        Vector3 point;
        float x = Random.Range(bounds.min.x + padding, bounds.max.x - padding);
        float z = Random.Range(bounds.min.z + padding, bounds.max.z - padding);
        point = new Vector3(x, spawnHeight, z);
        var i = Physics.OverlapSphere(point, distanceBetweenObjects, layer);

        if (i.Length > 0)
        {
            return GetRandomPointInBounds(bounds, distanceBetweenObjects, layer, spawnHeight);
        }
        else
        {
            return point;
        }
    }

    public void GenerateObjects(int number, float distance, List<GameObject> objects, LayerMask layer, float spawnHeight)
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 spawnPosition = GetRandomPointInBounds(targetCollider.bounds, distance, layer, spawnHeight);
            Instantiate(objects[Random.Range(0, objects.Count)], spawnPosition, Quaternion.identity);

        }
    }
}
