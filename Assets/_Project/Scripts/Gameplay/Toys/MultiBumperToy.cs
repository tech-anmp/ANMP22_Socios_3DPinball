using UnityEngine;

public class MultiBumperToy : MultiToyBase<Bumper>
{
    [SerializeField]
    private Bumper[] m_Bumpers;

    //private List<Bumper> m_HittedBumpers = new List<Bumper>();  

    /*protected override void Update()
    {
        if (!m_IsActivated)
            return;

        // Check if all bumpers were hitted, if so, then send points and deactivate
        if (m_HittedBumpers.Count == m_Bumpers.Length)
        {
            SendPoints();
            DeActivate();
        }
    }*/

    /*public override void Activate()
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
    }*/

    /*public override void DeActivate()
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
    }*/

    /*public override void ResetToy()
    {
        m_IsActivated = false;
        m_HittedBumpers.Clear();

        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                m_Bumpers[i].OnHit -= OnBumperHitted;
                m_Bumpers[i].ResetToyComponent();
            }
        }
    }*/

    /*private void OnBumperHitted(ToyComponentBase ToyComponent)
    {
        Bumper hittedBumper = ToyComponent as Bumper;
        if (hittedBumper == null)
            return;

        if (m_Bumpers != null && m_Bumpers.Length > 0)
        {
            for (int i = 0; i < m_Bumpers.Length; i++)
            {
                if (m_Bumpers[i] == hittedBumper && !m_HittedBumpers.Contains(hittedBumper))
                {
                    m_HittedBumpers.Add(hittedBumper);

                    //Bumper will not be active until all bumpers are hitted
                    m_Bumpers[i].SetActive(false);
                }
            }
        }
    }*/

    public override Bumper[] GetToyComponents()
    {
        return m_Bumpers;
    }
}
