// MoveDestination.cs
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 1;
    public Transform goal;

    private Animator anim;

    Vector3 lastPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
        
    }

    private void Update()
    {   
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;





    }
}