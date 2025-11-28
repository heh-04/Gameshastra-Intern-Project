using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    protected GameObject player;
    protected NavMeshAgent agent;

    public LayerMask excludeLayers;

    [SerializeField] protected HealthData healthData;
    [SerializeField] protected EnemyState currentState;
    [SerializeField] protected float enemyHeight = 2f;
    [SerializeField] protected float enemySpeed = 2f;
    [SerializeField] protected float enemyStoppingDistance = 2f;
    [SerializeField] protected float detectRange = 10f;
    [SerializeField] protected float attackRange = 1f;

    public float currentHealth { get { return currentHealth; }  set { currentHealth = value; } }
    public float maxHealth;

    protected void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        maxHealth = healthData.maxHealth;
        currentHealth = healthData.startHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemySpeed;
        player = GameObject.FindWithTag("Player");
        currentState = EnemyState.Idle;
    }

    protected virtual void Update()
    {
        HandleEnemyStates();
    }

    protected virtual void HandleEnemyStates()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
        }
    }

    protected bool WithinDetectRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectRange)
        {
            return true;
        }
        return false;
    }

    protected virtual bool HasLOS()
    {
        RaycastHit hit;

        Vector3 offsetPosition = transform.position + new Vector3(0, enemyHeight, 0);
        Vector3 direction = (player.transform.position - offsetPosition);

        if (Physics.Raycast(offsetPosition, direction, out hit, 50f, excludeLayers))
        {
            if (hit.collider.gameObject == player)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    protected virtual void RotationTowardsPlayer()
    {
        transform.LookAt(player.transform);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public virtual void Heal(int healAmount)
    {
        currentHealth += healAmount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Charging,
    Attacking,
    Patrolling,
    CalculatingNearestPointToPatrol,
    ReturningToPatrol
}
