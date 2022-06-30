using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    [SerializeField]
    private string m_CompareTag = "Ball";
    [SerializeField, Range(0,100)]
    private float m_Force = 50.0f;
    [SerializeField]
    private AudioClip m_AudioClip;

    private AudioSource m_AudioSource;
    private Rigidbody m_Ball;
    private bool m_IsReady;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(m_CompareTag))
        {
            m_Ball = other.GetComponent<Rigidbody>();
            if (m_Ball) m_IsReady = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_CompareTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == m_Ball)
            {
                m_Ball = null;
                m_IsReady = false;
            }
        }
    }

    public void Push()
    {
        if(m_Ball && m_IsReady)
        {
            m_AudioSource.PlayOneShot(m_AudioClip);
            m_Ball.AddForce(m_Force * transform.forward);
            m_IsReady = false;
        }
    }
}
