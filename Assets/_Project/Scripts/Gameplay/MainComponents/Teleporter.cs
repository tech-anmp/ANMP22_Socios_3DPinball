using UnityEngine;

public class Teleporter : ToyComponentBase
{
    [SerializeField]
    private Transform m_Destination;

    protected override void Start()
    {
        base.Start();

        m_IsActivated = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(m_CompareTag))
            return;

        if (!m_IsActivated)
            return;

        if (OnHit != null)
            OnHit(this);

        PlayAudioClip();

        Rigidbody ball = other.GetComponent<Rigidbody>();
        Teleport(ball);
    }

    private void Teleport(Rigidbody Body)
    {
        if (!Body)
            return;

        Body.isKinematic = true;
        Body.transform.position = m_Destination.position;
        Body.isKinematic = false;
    }
}
