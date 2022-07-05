using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Flipper : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_AudioClip;

    private AudioSource m_AudioSource;
    private HingeJoint m_HingeJoint;
    private bool m_IsFlipped;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_HingeJoint = GetComponent<HingeJoint>();
    }

    public void Flip()
    {
        m_IsFlipped = true;
        m_AudioSource.PlayOneShot(m_AudioClip);
    }

    public void FlipBack()
    {
        m_IsFlipped = false;
    }

    private void FixedUpdate()
    {
        JointSpring hingeSpring = m_HingeJoint.spring;                             // Prevent Flipper stuck when flipper need to go back his init position
        hingeSpring.spring = Random.Range(1.99f, 2.01f);
        m_HingeJoint.spring = hingeSpring;
        JointMotor motor = m_HingeJoint.motor;

        if (m_IsFlipped)
        {
            m_HingeJoint.motor = motor;
            m_HingeJoint.useMotor = true;
        }
        else
        {
            motor = m_HingeJoint.motor;
            m_HingeJoint.motor = motor;
            m_HingeJoint.useMotor = false;
        }
    }
}
