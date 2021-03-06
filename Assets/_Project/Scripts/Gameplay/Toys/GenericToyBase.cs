using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericToyBase<T> : ToyBase where T : ToyComponentBase
{
    [Header("Debug Gizmos")]
    [SerializeField]
    private Color m_GizmosColor = Color.blue;
    [SerializeField]
    private float m_GizmosSize = 0.05f;

    [Header("Delay")]
    [SerializeField]
    private bool m_DelayRestart;
    [SerializeField]
    private float m_DelayRestartTime = 0.5f;

    protected List<ToyComponentBase> m_HittedComponents = new List<ToyComponentBase>();

    protected virtual void Update()
    {
        if (!m_IsActivated)
            return;

        // Check if all bumpers were hitted, if so, then send points and deactivate
        if (m_HittedComponents.Count == GetToyComponents().Length)
        {
            if (m_SendPoints)
            {
                SendPoints();
                PlayAudioClip();
            }

            if (m_ResetOnCompleted)
                Restart();
            else
                DeActivate();
        }
    }

    public override void Activate()
    {
        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                GetToyComponents()[i].OnHit += OnToyComponentHitted;
                GetToyComponents()[i].SetActive(true);
            }
        }

        m_IsActivated = true;
    }

    public override void DeActivate()
    {
        m_IsActivated = false;
        m_HittedComponents.Clear();

        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                GetToyComponents()[i].OnHit -= OnToyComponentHitted;
                GetToyComponents()[i].SetActive(false);
                GetToyComponents()[i].ResetToyComponent();
            }
        }
    }

    public override void Restart()
    {
        if (m_DelayRestart)
            StartCoroutine(DelayedRestart());
        else
        {
            DeActivate();
            Activate();
        }
    }

    protected virtual void OnToyComponentHitted(ToyComponentBase ToyComponent)
    {
        T hittedObject = ToyComponent as T;
        if (hittedObject == null)
            return;

        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                if (GetToyComponents()[i] == hittedObject && !m_HittedComponents.Contains(hittedObject))
                {
                    m_HittedComponents.Add(hittedObject);

                    //ToyComponent will not be active until all bumpers are hitted
                    GetToyComponents()[i].SetActive(false);
                }
            }
        }
    }

    public abstract T[] GetToyComponents();

    private IEnumerator DelayedRestart()
    {
        DeActivate();
        yield return new WaitForSeconds(m_DelayRestartTime);
        Activate();
    }

    protected void OnDrawGizmosSelected()
    {
        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                Gizmos.color = m_GizmosColor;
                if (GetToyComponents()[i] != null)
                    Gizmos.DrawSphere(GetToyComponents()[i].transform.position, m_GizmosSize);
            }
        }
    }
}