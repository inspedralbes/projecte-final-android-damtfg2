using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    float m_ForceToAdd = 100;

    void OnTriggerStay(Collider other)
    {
        if (!enabled) return;
        if (other.gameObject.GetComponentInParent<ServerObject>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position) * m_ForceToAdd);
        }
    }
}