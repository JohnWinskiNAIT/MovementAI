using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    Rigidbody rbody;
    [SerializeField] GameObject detector;
    [SerializeField] float forceAmount = 7f;
    [SerializeField] LayerMask myMask;
    
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
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall");
            if (Physics.BoxCast(transform.position, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, Quaternion.identity, 1.5f, myMask))
            {
                transform.Rotate(Vector3.up, 90);
            }
            else
            {
                rbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            }
            
            
        }
    }
}
