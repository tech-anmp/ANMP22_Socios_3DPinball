using System;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    private float m_BumperForce = 0.3f;
    [SerializeField]
    private string m_CompareTag = "Ball";
    [SerializeField]
    private GameObject m_ActivationIndicator;
    [SerializeField]
    private AudioClip m_AudioClip;

    private bool m_IsActive;
    private AudioSource m_AudioSource;

    public Action<Bumper> OnHit;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        ResetBumper();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(m_CompareTag))
            return;

        ContactPoint[] tmpContact = collision.contacts;

        if(m_IsActive)
        {
            if (m_ActivationIndicator) m_ActivationIndicator.SetActive(true);

            if (OnHit != null)
                OnHit(this);
        }

        m_AudioSource.PlayOneShot(m_AudioClip);

        foreach (ContactPoint contact in tmpContact)
        {
            // Access rigidbody Component
            Rigidbody rb = contact.otherCollider.GetComponent<Rigidbody>();
            // save the collision.relativeVelocity.magnitude value
            float t = collision.relativeVelocity.magnitude;
            // reduce the velocity at the impact. Better feeling with the slingshot
            rb.velocity = new Vector3(rb.velocity.x * .25f, rb.velocity.y * .25f, rb.velocity.z * .25f);
            // Add Force
            rb.AddForce(-1 * contact.normal * m_BumperForce, ForceMode.VelocityChange);
        }
    }

    public void ResetBumper()
    {
        if (m_ActivationIndicator) m_ActivationIndicator.SetActive(false);
    }

    public void SetActive(bool IsActive)
    {
        m_IsActive = IsActive;
    }
}
