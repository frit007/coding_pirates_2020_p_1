using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
public class Enemy : Living {

    public Transform target;
    private NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public float fireDistance = 1f;
    
    public float canShootAt = 0;
    public float fireRate = 0.33f;

    public float randomFire = 2;

    Animator animator;

    BulletScript bullet;


    [Header("Prefabs")]
    public Transform bulletPrefab;
    public Transform casingPrefab;

    [Tooltip("How much force is applied to the bullet when shooting.")]
    public float bulletForce = 400.0f;


    [System.Serializable]
    public class spawnpoints {
        [Header("Spawnpoints")]
        //Array holding casing spawn points 
        //(some weapons use more than one casing spawn)
        //Casing spawn point array
        public Transform casingSpawnPoint;
        //Bullet prefab spawn from this point
        public Transform bulletSpawnPoint;
    }
    public spawnpoints Spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //character = GetComponent<ThirdPersonCharacter>();
        agent = GetComponent<NavMeshAgent>();
        //agent.
        //agent.updatePosition = false;
        agent.updateRotation = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            agent.SetDestination(target.transform.position);
        }

        if (agent.remainingDistance > fireDistance) {
            character.Move(agent.desiredVelocity, false, false);

            agent.isStopped = false;
        } else {
            // continue rotating towards character
            character.Move(Vector3.zero, false, false);
            //agent.updatePosition = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position, Vector3.up), Time.deltaTime * 5);
            agent.isStopped = true;
            Shoot();
        }
    }

    void Shoot() {
        if(canShootAt < Time.time) {
            canShootAt = Time.time + fireRate;
            StartCoroutine(ShootAnimation());
        }
    }

    IEnumerator ShootAnimation() {
        yield return new WaitForSeconds(0.1f);

        animator.SetTrigger("fire");

        //Spawn bullet from bullet spawnpoint
        var bullet = (Transform)Instantiate(
            bulletPrefab,
            Spawnpoints.bulletSpawnPoint.transform.position,
            Spawnpoints.bulletSpawnPoint.transform.rotation);

        

        //Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity =
            (target.position - Spawnpoints.bulletSpawnPoint.position + Random.insideUnitSphere * randomFire) * bulletForce;

        //Spawn casing prefab at spawnpoint
        Instantiate(casingPrefab,
            Spawnpoints.casingSpawnPoint.transform.position,
            Spawnpoints.casingSpawnPoint.transform.rotation);
    }
    
}
