using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ToyComponentBase : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField]
    protected string m_CompareTag = "Ball";

    [Header("Score")]
    [SerializeField]
    protected int m_Points = 10;

    [Header("Audio")]
    [SerializeField]
    protected AudioClip m_AudioClip;

    //[Header("Difficulty")]
    //[SerializeField]
    //private bool m_DisableOnHit;

    protected bool m_IsActivated;
    protected AudioSource m_AudioSource;
    protected IActionBase[] m_Actions;

    public Action<ToyComponentBase> OnHit;

    public int Points { get => m_Points; }

    protected virtual void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Actions =  GetComponentsInChildren<IActionBase>();
    }

    protected virtual void OnCollisionEnter(Collision Collision) { }

    public void PlayAudioClip()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }

    public virtual void ResetToyComponent() 
    {
        StopActions();
    }

    public virtual void SetActive(bool Value)
    {
        m_IsActivated = Value;
    }

    public virtual void StartActions()
    {
        if(m_Actions != null && m_Actions.Length > 0)
        {
            for (int i = 0; i < m_Actions.Length; i++)
            {
                m_Actions[i].StartAction();
            }
        }
    }

    public virtual void StopActions()
    {
        if (m_Actions != null && m_Actions.Length > 0)
        {
            for (int i = 0; i < m_Actions.Length; i++)
            {
                m_Actions[i].StopAction();
            }
        }
    }
}