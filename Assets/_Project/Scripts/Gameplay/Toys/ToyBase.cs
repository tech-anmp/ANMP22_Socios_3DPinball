using System;
using UnityEngine;

public abstract class ToyBase : MonoBehaviour
{
    [SerializeField]
    private int m_PointsToGive = 10;

    protected bool m_IsActivated;

    public Action<ToyBase, int> OnSendPoints;

    public bool IsActivated { get => m_IsActivated; }

    public virtual void SendPoints()
    {
        if (OnSendPoints != null)
            OnSendPoints(this, m_PointsToGive);
    }

    public abstract void Activate();

    public abstract void DeActivate();

    public abstract void ResetToy();
}