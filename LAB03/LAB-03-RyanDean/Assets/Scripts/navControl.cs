using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navControl : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject clickParticles;
    public bool isMoving = true;
    public float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        animator = GetComponent<Animator>();
        animator.speed = speed * 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                Instantiate(clickParticles, hit.point, Quaternion.AngleAxis(-90, new Vector3(1, 0, 0)));
                agent.SetDestination(hit.point);
            }
        }
        if (isMoving)
        {
            agent.SetDestination(target.transform.position);
        }
        else agent.SetDestination(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dragon")
        {
            isMoving = false;
            animator.SetTrigger("ATTACK");
            animator.speed = 1f;
            transform.LookAt(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Dragon")
        {
            isMoving = true;
            animator.SetTrigger("WALK");
            animator.speed = speed * 1.2f;
        }
    }
}
