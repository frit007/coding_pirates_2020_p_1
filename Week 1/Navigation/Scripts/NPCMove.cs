using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NPCMove : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    public ThirdPersonCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            agent.SetDestination(target.transform.position);
        }

        if(agent.remainingDistance > agent.stoppingDistance) {
            character.Move(agent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }
    }
}
