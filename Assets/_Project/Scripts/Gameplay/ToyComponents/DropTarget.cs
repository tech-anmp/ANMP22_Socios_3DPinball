using UnityEngine;

public class DropTarget : ToyComponentBase
{
    [Header("Display")]
    [SerializeField]
    private GameObject m_DropTarget;

    private Collider m_Collider;

    protected override void Start()
    {
        base.Start();

        m_Collider = GetComponent<Collider>();
    }

    protected override void OnCollisionEnter(Collision Collision)
    {
        if (!Collision.gameObject.CompareTag(m_CompareTag))
            return;

        if (OnHit != null)
            OnHit(this);

        PlayAudioClip();

        DropTargetObject();
    }

    public override void ResetToyComponent()
    {
        base.ResetToyComponent();

        m_DropTarget.SetActive(true);
        m_Collider.enabled = true;
        m_Collider.isTrigger = false;
    }

    private void DropTargetObject()
    {
        m_Collider.enabled = false;
        m_Collider.isTrigger = false;
        m_DropTarget.SetActive(false);
    }
}
