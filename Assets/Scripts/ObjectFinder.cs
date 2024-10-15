using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] LayerMask myMask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            if (!Physics.Linecast(transform.position, other.transform.position, out hit, myMask))
            {
                SendMessageUpwards("MoveToward", other.gameObject);
            }
            
        }
        else if (other.gameObject.tag == "Danger")
        {
            SendMessageUpwards("MoveAway", other.gameObject);
        }
    }
}
