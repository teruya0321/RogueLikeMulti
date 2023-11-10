using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    List<Transform> targets = new List<Transform>();

    float targetDistance = 0;

    EnemyManager manager;

    float targetSetTimer;

    int HP;
    int ATK;
    int SPEED;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        targets = GameManager.MainGameManager.players;

        TargetSet();
    }

    private void OnEnable()
    {
        manager = GameObject.Find("EnemySpawn").GetComponent<EnemyManager>();

        manager.AddEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        targetSetTimer += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if(targetSetTimer >= 3)
        {
            TargetSet();

            targetSetTimer = 0;
        }
    }

    void TargetSet()
    {
        targetDistance = 500;

        foreach (Transform t in targets)
        {
            float f = Vector3.Distance(transform.position, t.position);

            if (f < targetDistance)
            {
                targetDistance = f;

                target = t;
            }
        }
    }

    public void GetDamage(int damage)
    {
        HP -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        manager.DestroyEnemy(gameObject);
    }
}
