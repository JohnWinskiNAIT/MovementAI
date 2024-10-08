using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the front trigger detects an object
        if (other.gameObject.tag == "Wall")
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
    }
}
