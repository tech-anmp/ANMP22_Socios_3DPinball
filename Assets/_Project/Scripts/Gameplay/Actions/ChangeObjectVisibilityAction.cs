using System.Collections;
using UnityEngine;

public class ChangeObjectVisibilityAction : MonoBehaviour, IActionBase
{
    [Header("AutoActivation")]
    [SerializeField]
    private bool m_IsActionEnabled;

    [Header("Display")]
    [SerializeField]
    private GameObject m_DropTarget;
    [SerializeField]
    private Collider m_Collider;

    [Header("Delay")]
    [SerializeField]
    private bool m_DelayStop;
    [SerializeField]
    private float m_DelayStopTime = 0.5f;

    public void StartAction()
    {
        if (m_IsActionEnabled)
            StartSetVisibility();
    }

    public void StopAction()
    {
        if (!m_IsActionEnabled)
            return;

        if (m_DelayStop)
            StartCoroutine(DelayedStopAction(m_DelayStopTime));
        else
            StopSetVisibility();
    }

    private IEnumerator DelayedStopAction(float Time)
    {
        yield return new WaitForSeconds(Time);

        StopSetVisibility();
    }

    private void StartSetVisibility()
    {
        m_Collider.enabled = false;
        m_Collider.isTrigger = false;
        m_DropTarget.SetActive(false);
    }

    private void StopSetVisibility()
    {
        m_DropTarget.SetActive(true);
        m_Collider.enabled = true;
        m_Collider.isTrigger = false;
    }
}