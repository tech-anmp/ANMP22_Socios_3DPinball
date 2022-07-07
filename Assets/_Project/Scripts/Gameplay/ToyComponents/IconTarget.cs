using TMPro;
using UnityEngine;

public class IconTarget : ToyComponentBase
{
    [Header("Display")]
    [SerializeField]
    private TextMeshPro m_PointsDisplay;

    protected override void Start()
    {
        base.Start();

        ResetToyComponent();

        if (m_PointsDisplay) m_PointsDisplay.text = m_Points.ToString();
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
