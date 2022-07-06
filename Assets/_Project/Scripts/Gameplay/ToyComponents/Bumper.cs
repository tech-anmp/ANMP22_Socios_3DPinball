using UnityEngine;

public class Bumper : ToyComponentBase
{
    [Header("Physics")]
    [SerializeField]
    private float m_BumperForce = 0.3f;

    protected override void Start()
    {
        base.Start();

        ResetToyComponent();
    }

    protected override void OnCollisionEnter(Collision Collision)
    {
        if (!Collision.gameObject.CompareTag(m_CompareTag))
            return;

        ContactPoint[] tmpContact = Collision.contacts;

        if (m_IsActivated)
            StartActions();

        if (OnHit != null)
            OnHit(this);

        PlayAudioClip();

        foreach (ContactPoint contact in tmpContact)
        {
            // Access rigidbody Component
            Rigidbody rb = contact.otherCollider.GetComponent<Rigidbody>();
            // save the collision.relativeVelocity.magnitude value
            float t = Collision.relativeVelocity.magnitude;
            // reduce the velocity at the impact. Better feeling with the slingshot
            rb.velocity = new Vector3(rb.velocity.x * .25f, rb.velocity.y * .25f, rb.velocity.z * .25f);
            // Add Force
            rb.AddForce(-1 * contact.normal * m_BumperForce, ForceMode.VelocityChange);
        }
    }
}