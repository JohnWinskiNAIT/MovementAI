using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AdvancedPlayerAI : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    Rigidbody rbody;
    [SerializeField] GameObject detector;
    [SerializeField] float forceAmount = 7f;
    [SerializeField] LayerMask myMask;
    [SerializeField] GameObject playerView;

    [SerializeField] bool leftCast, rightCast;

    RaycastHit hit;

    [SerializeField] List<GameObject> targets;
    float targetDistance;
    GameObject closestTarget;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        targetDistance = 10000;
        closestTarget = null;
        foreach (GameObject target in targets)
        {
            if (!Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), target.transform.position - transform.position, out hit, Quaternion.identity, Vector3.Distance(transform.position, target.transform.position), myMask))
            {
                if (Vector3.Distance(transform.position, target.transform.position) < targetDistance)
                {
                    targetDistance = Vector3.Distance(transform.position, target.transform.position);
                    closestTarget = target;
                }                
            }
        }

        if (closestTarget != null)
        {
            if (closestTarget.tag == "Pickup")
            {
                SendMessageUpwards("MoveToward", closestTarget.gameObject);
            }
            else if (closestTarget.tag == "Hunter")
            {
                SendMessageUpwards("MoveAway", closestTarget.gameObject);
            }
            
        }

    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);

        
    }

    public void TurnPlayer()
    {
        // If the object in front of the AI is on the wall layer
        if (Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out hit, Quaternion.identity, 1.5f, myMask))
        {
            transform.LookAt(transform.position - hit.normal);

            // Look left and right for walls
            leftCast = Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), -transform.right, Quaternion.identity, 8.0f, myMask);
            rightCast = Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.right, Quaternion.identity, 8.0f, myMask);

            if (!rightCast && leftCast)
            {
                transform.Rotate(Vector3.up, 90);
            }
            else if (rightCast && !leftCast)
            {
                transform.Rotate(Vector3.up, -90);
            }
            else if (leftCast && rightCast)
            {
                transform.Rotate(Vector3.up, 180);
            }
            else
            {
                transform.Rotate(Vector3.up, 90);
            }
        }
        else
        {
            rbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
        }
    }

    public void MoveToward(GameObject target)
    {
        
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }

    public void MoveAway(GameObject target)
    {
        if (!Physics.BoxCast(playerView.transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.position - target.transform.position, out hit, Quaternion.identity, 3.0f, myMask))
        {
            Vector3 targetVector = transform.position + (transform.position - target.transform.position);            
            transform.LookAt(new Vector3(targetVector.x, transform.position.y, targetVector.z));
        }        
    }

    public void AddTarget(GameObject target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
        }
    }

    public void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 1.5f)
            {
                other.transform.root.gameObject.SetActive(false);
                RemoveTarget(other.transform.root.gameObject);
            }

        }
    }
}
