using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{

    [SerializeField]
    public PathManager pathManager;

    List<Waypoint> thePath;
    Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    private Animator animator;
    public bool isWalking = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        thePath = pathManager.GetPath();
        if(thePath != null && thePath.Count > 0)
        {
            // set starting target to the first waypoint
            target = thePath[0];
        }
    }

    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        if (stepSize > 0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
        float distanceToTarget = Vector3.Distance(transform.position, target.pos);
        if (distanceToTarget < stepSize)
        {
            // we will overshoot the target
        }
        Vector3 movDir = Vector3.forward;
        transform.Translate(movDir * stepSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        target = pathManager.GetNextTarget();
        if (other.GetComponent<PathController>())
        {
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWalking = !isWalking;
        }
        if (!isWalking) MoveSpeed = 0;
        else MoveSpeed = 3;
        rotateTowardsTarget();
        moveForward();
    }
}
