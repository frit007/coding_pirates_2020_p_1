using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NPCMoveSimple : MonoBehaviour
{
    // Dette er variablen vi kommer til at sætte i inspector vinduet.
    // Det kommer til at blive sat en GameObject, hvilket kunne være en anden cube eller spilleren.
    public Transform target;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // find NavMeshAgent på figuren
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // hver frame opdater hvor npc prøver at nå hen til
        agent.SetDestination(target.transform.position);
    }
}
