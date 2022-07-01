using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private float m_MaxSpeed = 25;
    [SerializeField]
    private float m_MinMagAudio = 1;

    private AudioSource m_AudioSource;
    private Rigidbody m_RigidBody;
    private bool m_Once;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (m_RigidBody.velocity.magnitude > m_MaxSpeed)                                       // Limit ball speed.
        {
            m_RigidBody.velocity = m_RigidBody.velocity.normalized * m_MaxSpeed;
        }

        if (m_RigidBody.velocity.magnitude > m_MinMagAudio && m_Once)                     // Play the roll sound.
        {
            m_AudioSource.Play();
            m_Once = false;
        }
        else if (m_RigidBody.velocity.magnitude <= m_MinMagAudio && !m_Once)              // Stop the roll sound.
        {
            m_AudioSource.Stop();
            m_AudioSource.pitch = 1;
            m_Once = true;
        }

        if (!m_Once)
        {
            m_AudioSource.pitch = m_RigidBody.velocity.magnitude / 2.5f;                            // When ball accelerate the pitch increase. 
        }
    }
}
