using UnityEngine;

public class DropTarget : ToyComponentBase
{
    protected override void OnCollisionEnter(Collision Collision)
    {
        if (!Collision.gameObject.CompareTag(m_CompareTag))
            return;

        if (m_IsActivated)
        {
            StartActions();

            if (OnHit != null)
                OnHit(this);

            PlayAudioClip();
        }
    }
}
