using System.Collections;
using UnityEngine;

public class ShootingEnemy2D : MonoBehaviour
{
    public Vector2 spawnOffset;
    public int secondsBetweenShots = 2;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Attack");
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenShots);
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        GameObject bullet = TrunkBulletPool2D.instance.pool.Get();
        bullet.transform.SetPositionAndRotation(transform.position + (Vector3)spawnOffset, Quaternion.identity);
    }
}
