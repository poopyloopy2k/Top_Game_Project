using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasingEnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMin = 7f;
    [SerializeField] private float roamingDistanceMax = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float chaseDistance = 10f; // Дистанция преследования

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;
    private Transform player; // Ссылка на игрока

    private enum State
    {
        Roaming,
        Chasing
    }

    private void Start()
    {
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Поиск игрока по тегу
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Roaming:
                RoamingBehavior();
                CheckForPlayer();
                break;
            case State.Chasing:
                ChasingBehavior();
                break;
        }
    }

    private void RoamingBehavior()
    {
        roamingTime -= Time.deltaTime;
        if (roamingTime < 0)
        {
            Roaming();
            roamingTime = roamingTimerMax;
        }
    }

    private void ChasingBehavior()
    {
        navMeshAgent.SetDestination(player.position);
        changeFacingDirection(transform.position, player.position);

        if (Vector3.Distance(transform.position, player.position) > chaseDistance)
        {
            state = State.Roaming;
        }
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= chaseDistance)
        {
            state = State.Chasing;
        }
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
        changeFacingDirection(startingPosition, roamPosition);
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void changeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
