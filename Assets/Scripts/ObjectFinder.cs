using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] LayerMask myMask;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup" ||
            other.gameObject.tag == "Hunter" ||
            other.gameObject.tag == "Runner")
        {
            SendMessageUpwards("AddTarget", other.transform.root.gameObject);           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickup" ||
            other.gameObject.tag == "Hunter" ||
            other.gameObject.tag == "Runner")
        {
            SendMessageUpwards("RemoveTarget", other.transform.root.gameObject);
        }
    }
}
