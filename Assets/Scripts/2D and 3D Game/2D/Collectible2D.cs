using UnityEngine;

public class Collectible2D : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Collected");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
