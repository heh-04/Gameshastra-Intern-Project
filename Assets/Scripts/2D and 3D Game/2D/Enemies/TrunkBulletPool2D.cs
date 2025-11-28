using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrunkBulletPool2D : MonoBehaviour
{
    public static TrunkBulletPool2D instance;

    public GameObject bulletPrefab;
    public ObjectPool<GameObject> pool;
    public int initialNoOfPoolItems = 6;
    private List<GameObject> initialPoolItems;
    private Transform thisTransform;


    void Awake()
    {
        thisTransform = gameObject.GetComponent<Transform>();

        if (instance == null)
        {
            instance = this;
        }

        pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50
        );

        PreLoadObjectPool(initialNoOfPoolItems);
    }

    private GameObject CreateItem()
    {
        GameObject gameObject = Instantiate(bulletPrefab);
        gameObject.name = "PooledTrunkBullet";
        gameObject.transform.SetParent(thisTransform);
        gameObject.SetActive(false);
        return gameObject;
    }

    private void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void OnRelease(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroyItem(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private void PreLoadObjectPool(int n)
    {
        initialPoolItems = new List<GameObject>();
        for (int i = n; i > 0; i--)
        {
            GameObject bullet = pool.Get();
            initialPoolItems.Add(bullet);
        }
        foreach (GameObject bullet in initialPoolItems)
        {
            pool.Release(bullet);
        }
    }
}