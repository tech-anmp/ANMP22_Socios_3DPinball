using UnityEngine;

public class IconTarget : ToyComponentBase
{
    [Header("Display")]
    [SerializeField]
    private Material m_MaterialRef;

    protected override void Start()
    {
        base.Start();

        ResetToyComponent();
    }

    protected virtual void OnTriggerEnter(Collider Collision)
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
