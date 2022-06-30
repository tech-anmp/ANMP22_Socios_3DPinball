using System.Collections.Generic;
using UnityEngine;

public class MultiBumperToy : ToyBase
{
    [SerializeField]
    private Bumper[] m_Bumpers;

    private List<Bumper> m_HittedBumpers = new List<Bumper>();  

    private void Update()
    {
        if (!m_IsActivated)
            return;

        //TODO : Check if all bumpers were hitted, if so, then send points and deactivate
        if (m_HittedBumpers.Count == m_Bumpers.Length)
        {
            SendPoints();
            DeActivate();
        }
    }

    public override void Activate()
    {
        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                m_Bumpers[i].OnHit += OnBumperHitted;
                m_Bumpers[i].SetActive(true);
            }
        }

        m_IsActivated = true;
    }

    public override void DeActivate()
    {
        m_IsActivated = false;

        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                m_Bumpers[i].OnHit -= OnBumperHitted;
                m_Bumpers[i].SetActive(false);
            }
        }
    }

    public override void ResetToy()
    {
        m_IsActivated = false;
        m_HittedBumpers.Clear();

        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                m_Bumpers[i].OnHit -= OnBumperHitted;
                m_Bumpers[i].ResetBumper();
            }
        }
    }

    private void OnBumperHitted(Bumper Bumper)
    {
        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                if (m_Bumpers[i] == Bumper && !m_HittedBumpers.Contains(Bumper))
                {
                    m_HittedBumpers.Add(Bumper);

                    //Bumper will not be active until all bumpers are hitted
                    m_Bumpers[i].SetActive(false);
                    Debug.Log("Added bumper : " + Bumper.gameObject.name);
                }
            }
        }
    }
}
