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

    public float stoppingDistance;
    public float retreatDistance;
    public float speed;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;



    private enum State
    {
        Roaming,
        Chasing
    }

    private void Start()
    {
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Поиск игрока по тегу

        timeBtwShots = startTimeBtwShots;

    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = speed;
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
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            //transform.position = this.transform.position;
            navMeshAgent.speed = 0;
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            navMeshAgent.speed = speed;
            retreating();
        }
        else
        {
            navMeshAgent.speed = speed;
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    public void retreating()
    {
        Vector3 retreatDirection = transform.position - player.position;
        retreatDirection.Normalize();

        Vector3 retreatPosition = transform.position + retreatDirection;
        navMeshAgent.SetDestination(retreatPosition);
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
