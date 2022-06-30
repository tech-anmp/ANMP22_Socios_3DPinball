using UnityEngine;

public class ActiveableWall : MonoBehaviour
{
    [SerializeField]
    private string m_CompareTag = "Ball";

    private MeshRenderer m_MeshRenderer;
    private Collider m_Collider;

    private bool m_IsBallEnter;

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_Collider = GetComponent<Collider>();
    }

    public void Activate()
    {
        m_Collider.isTrigger = false;
        m_MeshRenderer.enabled = true;
    }

    public void DeActivate()
    {
        m_Collider.isTrigger = true;
        m_MeshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(m_CompareTag))
            return;

        m_IsBallEnter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(m_CompareTag))
            return;

        if (!m_IsBallEnter)
            return;

        Activate();
        m_IsBallEnter = false;
    }
}
