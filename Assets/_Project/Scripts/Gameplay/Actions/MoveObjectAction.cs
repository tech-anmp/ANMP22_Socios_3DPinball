using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoveObjectAction : MonoBehaviour, IActionBase
{
    [Header("Display")]
    [SerializeField]
    private GameObject m_DropTarget;
    [SerializeField]
    private Collider m_Collider;
    [SerializeField]
    private Transform m_Destination;

    [SerializeField]
    private float m_Duration = 0.5f;

    [Header("Delay")]
    [SerializeField]
    private bool m_DelayStop;
    [SerializeField]
    private float m_DelayStopTime = 0.5f;

    private Vector3 m_StartLocation;

    private void Start()
    {
        m_StartLocation = m_DropTarget.transform.position;
    }

    public void StartAction()
    {
        StartMove();
    }

    public void StopAction()
    {
        if (m_DelayStop)
            StartCoroutine(DelayedStopAction(m_DelayStopTime));
        else
            StopMove();
    }

    private IEnumerator DelayedStopAction(float Time)
    {
        yield return new WaitForSeconds(Time);

        StopMove();
    }

    private void StartMove()
    {
        m_Collider.enabled = false;
        m_Collider.isTrigger = false;
        m_DropTarget.transform.DOMove(m_Destination.position, m_Duration)
            .OnComplete(() => m_DropTarget.SetActive(false));
        
    }

    private void StopMove()
    {
        m_DropTarget.SetActive(true);
        m_DropTarget.transform.DOMove(m_StartLocation, m_Duration)
            .OnComplete(() => { m_Collider.enabled = true; m_Collider.isTrigger = false; });
    }
}
