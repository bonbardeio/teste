using MobileFPS.Player;
using UnityEngine;
using UnityEngine.AI;

namespace MobileFPS.AI
{
    /// <summary>
    /// IA básica do soldado com estados: patrulhar, perseguir e atirar.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAIController : MonoBehaviour
    {
        private enum AIState { Patrol, Chase, Attack }

        [Header("Detecção")]
        [SerializeField] private Transform player;
        [SerializeField] private float detectionRange = 30f;
        [SerializeField] private float attackRange = 16f;

        [Header("Patrulha")]
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolWait = 1.5f;

        [Header("Ataque")]
        [SerializeField] private float fireRate = 1.2f;
        [SerializeField] private float damagePerShot = 8f;

        private NavMeshAgent agent;
        private PlayerHealth playerHealth;
        private AIState state;
        private int patrolIndex;
        private float nextShot;
        private float patrolTimer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            playerHealth = player.GetComponent<PlayerHealth>();
            state = AIState.Patrol;
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= attackRange)
                state = AIState.Attack;
            else if (distance <= detectionRange)
                state = AIState.Chase;
            else
                state = AIState.Patrol;

            switch (state)
            {
                case AIState.Patrol:
                    Patrol();
                    break;
                case AIState.Chase:
                    Chase();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
            }
        }

        private void Patrol()
        {
            if (patrolPoints == null || patrolPoints.Length == 0)
                return;

            if (!agent.hasPath || agent.remainingDistance < 0.3f)
            {
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= patrolWait)
                {
                    patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                    agent.SetDestination(patrolPoints[patrolIndex].position);
                    patrolTimer = 0f;
                }
            }
        }

        private void Chase()
        {
            agent.SetDestination(player.position);
        }

        private void Attack()
        {
            agent.SetDestination(transform.position);
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0f;
            if (lookPos.sqrMagnitude > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 8f);
            }

            if (Time.time >= nextShot)
            {
                nextShot = Time.time + (1f / fireRate);
                playerHealth.TakeDamage(damagePerShot);
            }
        }
    }
}
