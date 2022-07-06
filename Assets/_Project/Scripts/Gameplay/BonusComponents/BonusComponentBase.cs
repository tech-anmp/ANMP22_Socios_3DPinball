using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BonusComponentBase : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField]
    protected bool m_ResetOnBallReset;
    [SerializeField]
    protected bool m_ResetOnCompleted;

    [Header("Toys")]
    [SerializeField]
    private ToyBase[] m_Toys;

    [Header("Points")]
    [SerializeField]
    private int m_PointsToGive = 500;

    [Header("Audio")]
    [SerializeField]
    private AudioClip m_AudioClip;

    private List<ToyBase> m_ReceivedToys = new List<ToyBase>();
    private AudioSource m_AudioSource;
    protected bool m_IsActivated;

    public Action<int> OnSendBonus;

    public bool IsActivated { get => m_IsActivated; }
    public bool RestartOnBallReset { get => m_ResetOnBallReset; }
    public bool RestartOnCompleted { get => m_ResetOnCompleted; }

    protected virtual void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!m_IsActivated)
            return;

        if (m_ReceivedToys.Count == m_Toys.Length)
        {
            if (OnSendBonus != null)
                OnSendBonus(m_PointsToGive);

            PlayAudioClip();

            if (m_ResetOnCompleted)
                Restart();
            else
                DeActivate();
        }
    }

    public void Activate()
    {
        if(m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                m_Toys[i].OnSendPoints += OnReceivePoints;
            }
        }

        m_IsActivated = true;
    }

    public void DeActivate()
    {
        m_IsActivated = false;
        m_ReceivedToys.Clear();

        if (m_Toys != null && m_Toys.Length > 0)
        {
            for (int i = 0; i < m_Toys.Length; i++)
            {
                m_Toys[i].OnSendPoints -= OnReceivePoints;
            }
        }
    }

    public void Restart()
    {
        DeActivate();
        Activate();
    }

    private void OnReceivePoints(ToyBase Toy, int Points)
    {
        if(!m_ReceivedToys.Contains(Toy))
            m_ReceivedToys.Add(Toy);
    }


    public void PlayAudioClip()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}
