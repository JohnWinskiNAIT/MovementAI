using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    Rigidbody rbody;
    [SerializeField] GameObject detector;
    [SerializeField] float forceAmount = 10f;
    [SerializeField] LayerMask myMask;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * movementSpeed);

        //if (rbody != null)
        //{
        //    if (rbody.velocity.magnitude == 0)
        //    {
        //        transform.Rotate(Vector3.up, 180);
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall");
            if (Physics.BoxCast(detector.transform.position, new Vector3(0.5f, 1, 0.5f), transform.forward, Quaternion.identity, 1.5f, myMask) || Physics.CheckBox(detector.transform.position, new Vector3(0.5f, 1, 0.4f),Quaternion.identity, myMask))
            //if (Physics.Raycast(detector.transform.position, transform.forward, 1.5f))
            {
                transform.Rotate(Vector3.up, 90);
            }
            else
            {
                Debug.Log("Jump");
                rbody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
            }
            
            
        }
    }
}
