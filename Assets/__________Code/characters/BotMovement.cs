using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour, IBotMovement
{
    public float courseUpdateTime = .1f;
    public float attackAngleDotProd => botStats.AttackAngleDotProd;
    public float attackRange => botStats.AttackRange;
    [Space]
    public BotSight BotSight;
    public BotStats botStats;
    [Space]
    public bool inAttack = true;
    [Space]
    public Pipe_SoundsPlay Addon_Pipe_SoundsPlay;
    public ClipsCollection Addon_stepSound;

    public bool InDistanceForAttack => BotSight.CanSee ?
        BotSight.distanceToPlayer <= NavMeshAgent.stoppingDistance : false;
    public bool IsMoving => NavMeshAgent.velocity.sqrMagnitude > 4;

    NavMeshAgent NavMeshAgent;

    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = botStats.MovementSpeed;
        NavMeshAgent.stoppingDistance = attackRange;

        StartCoroutine(KeepWalking());
        //StartCoroutine(SoundWalking());
    }

    IEnumerator SoundWalking()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsMoving);
            if (Addon_Pipe_SoundsPlay != null && Addon_stepSound != null)
                Addon_Pipe_SoundsPlay.AddClip(new PlayClipData(Addon_stepSound, transform.position));
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator KeepWalking()
    {
        while (true)
        {
            yield return new WaitUntil(() => !inAttack && BotSight.CanSee);
            while (!inAttack && BotSight.CanSee)
            {
                if (!IsEnemyInRangeForAttack(BotSight.distanceToPlayer))
                {
                    Debug.Log("Kill!");
                    NavMeshAgent.SetDestination(PlayerSinglton.PlayerPosition);
                }
                yield return new WaitForSeconds(courseUpdateTime);
            }
        }
    }

    void FixedUpdate()
    {
        if (!inAttack && BotSight.CanSee && !IsEnemyInAngleForAttack())
        {
            var _lookRotation = Quaternion.LookRotation(BotSight.directionToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, _lookRotation, NavMeshAgent.angularSpeed / 90 * Time.deltaTime);
        }
    }

    bool IsEnemyInAngleForAttack()
    {
        return BotSight.dotProductToPlayer > attackAngleDotProd;
    }

    bool IsEnemyInRangeForAttack(float distanceToEnemy)
    {
        return distanceToEnemy < attackRange;
    }
}

public interface IBotMovement
{
    bool InDistanceForAttack { get; }
    bool IsMoving { get; }
}