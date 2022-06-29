using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    private float m_BumperForce = 0.3f;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] tmpContact = collision.contacts;

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
}
