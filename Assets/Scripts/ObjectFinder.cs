using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            SendMessageUpwards("MoveToward", other.gameObject);
        }
        else if (other.gameObject.tag == "Danger")
        {
            SendMessageUpwards("MoveAway", other.gameObject);
        }
    }
}
