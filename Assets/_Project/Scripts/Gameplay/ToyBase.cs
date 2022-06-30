using System;
using UnityEngine;

public abstract class ToyBase : MonoBehaviour
{
    [SerializeField]
    private int m_PointsToGive = 10;

    protected bool m_IsActivated;

    public Action<int> OnSendPoints;

    public bool IsActivated { get => m_IsActivated; }

    public virtual void SendPoints()
    {
        if (OnSendPoints != null)
            OnSendPoints(m_PointsToGive);
    }

    public abstract void Activate();

    public abstract void DeActivate();

    public abstract void ResetToy();
}