using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private float m_MaxSpeed = 25;

    private Rigidbody m_RigidBody;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_RigidBody.velocity.magnitude > m_MaxSpeed)                                       // Limit ball speed.
        {
            m_RigidBody.velocity = m_RigidBody.velocity.normalized * m_MaxSpeed;
        }
    }
}
