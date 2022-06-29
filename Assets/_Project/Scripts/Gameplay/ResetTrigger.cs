using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResetTrigger : MonoBehaviour
{
    public Action OnReset;

    [SerializeField]
    private string m_CompareTag = "Ball";

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(m_CompareTag))
        {
            if (OnReset != null)
                OnReset();
        }
    }
}
