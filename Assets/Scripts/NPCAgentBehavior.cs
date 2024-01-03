using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAgentBehavior : NPCAbilities
{
    private GameObject[] _destinations;
    private Vector3 _destinationPoint;

    [SerializeField] private Transform _player;
    [SerializeField] private NavMeshAgent _searcherAgent;

    [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;

    private Animator _searcherAnim;

    private bool _alreadyAttacked;

    [SerializeField] private float _sightRange, _attackRange;
    private bool _playerInSightRange, _playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        _destinations = GameObject.FindGameObjectsWithTag("Destination");
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _searcherAgent = GetComponent<NavMeshAgent>();
        _searcherAnim = GetComponent<Animator>();
        _searcherAnim.SetBool("Walk", true);

        PickRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (DidReachDestination())
        {
            PickRandomDestination();
        }

        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);

        if (!_playerInSightRange && !_playerInAttackRange) WalkToRandomDestination();
        if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
    }

    public override void PickRandomDestination()
    {
        int destinationIndex = Random.Range(0, _destinations.Length);
        _destinationPoint = _destinations[destinationIndex].transform.position;
    }

    public override void WalkToRandomDestination()
    {
        _searcherAgent.SetDestination(_destinationPoint);
        _searcherAgent.speed = 2;
    }

    public override bool DidReachDestination()
    {
        if (Vector3.Distance(_destinationPoint, transform.position) <= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChasePlayer()
    {
        _searcherAgent.speed = 4.5f;
        _searcherAgent.SetDestination(_player.position);
    }
}
