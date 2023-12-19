using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        targets = GameManager.MainGameManager.players;

        TargetSet();
    }
    public NavMeshAgent RequestAgent()
    {
        return agent;
    }

    private void OnEnable()
    {
        manager = GameManager.MainGameManager.enemyManager;
    }

    // Update is called once per frame
    void Update()
    {
        targetSetTimer += Time.deltaTime;
        if (target == null) TargetSet();
        agent.SetDestination(target.position);
    }

    private void LateUpdate()
    {
        if(targetSetTimer >= 3)
        {
            TargetSet();

            targetSetTimer = 0;
        }
    }

    public void EnemyTargetSet(Transform t)
    {
        target = t;
    }

    void TargetSet()
    {
        //Debug.Log(GetComponent<Healing>());
        if (GetComponent<Healing>() != null)
        {
            GetComponent<Healing>().EnemyHealing();
            return;
        }
        Debug.Log("âÒïúÇµÇ»Ç¢Ç∫ÅI");
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
