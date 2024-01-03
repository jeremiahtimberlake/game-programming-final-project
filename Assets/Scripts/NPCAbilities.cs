using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public abstract class NPCAbilities : MonoBehaviour
{
    public abstract void PickRandomDestination();

    public abstract void WalkToRandomDestination();

    public abstract bool DidReachDestination();
}

