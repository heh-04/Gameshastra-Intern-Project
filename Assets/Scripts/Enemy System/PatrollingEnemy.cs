using System.ComponentModel;
using UnityEngine;
using UnityEngine.Splines;

public class PatrollingEnemy : BaseEnemy
{
    protected SplineAnimate splineAnimate;
    protected Vector3 _nearestPointOnSpline = Vector3.zero;
    protected float splineT = 0f;
    protected bool splineIsReversing = false;

    protected override void Init()
    {
        base.Init();

        if(splineAnimate == null)
        {
            splineAnimate = GetComponent<SplineAnimate>();
        }

        currentState = EnemyState.Patrolling;
    }

    protected override void HandleEnemyStates()
    {
        base.HandleEnemyStates();
        switch (currentState)
        {
            case EnemyState.Idle:
                {
                    if(HasLOS() && WithinDetectRange())
                    {
                        agent.stoppingDistance = enemyStoppingDistance;
                        currentState = EnemyState.Chasing;
                    }

                    break;
                }

            case EnemyState.Chasing:
                {
                    Chase();
                    break;
                }

            case EnemyState.Patrolling:
                {
                    if (HasLOS() && WithinDetectRange())
                    {
                        splineAnimate.Pause();
                        agent.stoppingDistance = enemyStoppingDistance;
                        currentState = EnemyState.Chasing;
                    }

                    break;
                }

            case EnemyState.ReturningToPatrol:
                {
                    ReturnToPatrol(_nearestPointOnSpline, splineT);
                    agent.stoppingDistance = 0f;
                    break;
                }

            case EnemyState.CalculatingNearestPointToPatrol:
                {
                    GetNearestPointOnSpline();
                    break;
                }

            case EnemyState.Attacking:
                {
                    Attack();
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

        if (!HasLOS() || !WithinDetectRange())
        {
            currentState = EnemyState.CalculatingNearestPointToPatrol;
        }

        if(HasLOS() && CanAttack())
        {
            currentState = EnemyState.Attacking;
        }
    }

    protected void ReturnToPatrol(Vector3 nearestPoint, float t)
    {
        agent.SetDestination(nearestPoint);

        if (HasLOS() && WithinDetectRange())
        {
            agent.stoppingDistance = enemyStoppingDistance;
            currentState = EnemyState.Chasing;
            return;
        }

        else if (Vector3.Distance(transform.position, nearestPoint) < 0.5f)
        {
            splineAnimate.NormalizedTime = t;
            splineAnimate.Play();
            currentState = EnemyState.Patrolling;
        }
    }

    protected void Attack()
    {
        // Trigger Animation

        if(!CanAttack())
        {
            agent.stoppingDistance = enemyStoppingDistance;
            currentState = EnemyState.Chasing;
        }

        return;
    }

    protected bool CanAttack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void GetNearestPointOnSpline()
    {
        Vector3 localTransformPosition = splineAnimate.Container.transform.InverseTransformPoint(transform.position);
        SplineUtility.GetNearestPoint(splineAnimate.Container.Spline, localTransformPosition, out var localNearestPoint, out float t);
        _nearestPointOnSpline = splineAnimate.Container.transform.TransformPoint(localNearestPoint);
        splineT = t;
        currentState = EnemyState.ReturningToPatrol;
    }
}
