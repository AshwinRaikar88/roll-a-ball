using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Animator anim;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;

        anim = GetComponentInChildren<Animator>();
        if (anim)
        {
            anim.SetFloat("speed_f", navMeshAgent.speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)        
        {
            navMeshAgent.SetDestination(player.position);
            FacePlayer();
        }
        else {
            anim.SetFloat("speed_f", 0);
        }
    }

    void FacePlayer()
    {
        Vector3 direction = transform.position - player.position;
        direction.Normalize();        
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
