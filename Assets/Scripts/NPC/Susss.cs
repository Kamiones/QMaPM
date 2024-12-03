using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Susss : MonoBehaviour
{
    [HideInInspector] public Sospechoso sus;
    //public float speed = 3.5f;
    //public float wanderRadius = 10f; // Radio en el que el Susss vagará

    //private NavMeshAgent agent;
    //private bool isWandering = false;

    /*void Start()
    {      
        // Asegurarse de tener NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        
        // Configurar el agente
        agent.speed = speed;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
        agent.stoppingDistance = 0;

        // Comenzar a vagar
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        while (true)
        {
            if (!isWandering && agent.isOnNavMesh)
            {
                isWandering = true;
                
                // Encontrar un punto aleatorio cercano
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRadius;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, wanderRadius, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }

                // Esperar a que llegue al destino o pase cierto tiempo
                yield return new WaitForSeconds(Random.Range(3, 7));
                isWandering = false;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }*/

}