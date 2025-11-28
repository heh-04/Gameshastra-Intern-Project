using System.Collections.Specialized;
using UnityEngine;

public class ChargingEnemy : BaseEnemy
{
    [SerializeField] protected float chargeRange = 10f;
    [SerializeField] protected float chargeSpeed = 5f;

    protected override void HandleEnemyStates()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                {
                    agent.ResetPath();

                    if (HasLOS() && WithinDetectRange())
                    {
                        currentState = EnemyState.Chasing;
                    }

                    break;
                }

            case EnemyState.Chasing:
                {
                    Chase();

                    if (Vector3.Distance(transform.position, player.transform.position) < chargeRange)
                    {
                        agent.speed = chargeSpeed;
                        currentState = EnemyState.Charging;
                    }

                    break;
                }

            case EnemyState.Charging:
                {
                    Chase();

                    if (Vector3.Distance(transform.position, player.transform.position) >= chargeRange)
                    {
                        agent.speed = enemySpeed;
                        currentState = EnemyState.Chasing;
                    }

                    break;
                }
        }
    }

    protected void Chase()
    {
        if (HasLOS() && WithinDetectRange())
        {
            RotationTowardsPlayer();
            agent.SetDestination(player.transform.position);
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }
}
