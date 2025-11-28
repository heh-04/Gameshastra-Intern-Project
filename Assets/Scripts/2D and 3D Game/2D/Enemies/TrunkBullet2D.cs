using System.Collections;
using UnityEngine;

public class TrunkBullet2D : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 0.5f;
    public float maxBulletLifetime = 5f;

    private bool lifetimeEnded = true;

    private void OnEnable()
    {
        lifetimeEnded = false;
        StartCoroutine(DeactivateRoutine(maxBulletLifetime));
    }

    private void Update()
    {
        if (!lifetimeEnded)
        {
            transform.Translate(-Vector3.right * bulletSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lifetimeEnded = true;
        TrunkBulletPool2D.instance.pool.Release(gameObject);
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        lifetimeEnded = true;
        TrunkBulletPool2D.instance.pool.Release(gameObject);
    }
}
