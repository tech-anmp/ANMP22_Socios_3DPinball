using System.Collections.Generic;

public abstract class GenericToyBase<T> : ToyBase where T : ToyComponentBase
{
    protected List<ToyComponentBase> m_HittedComponents = new List<ToyComponentBase>();

    protected virtual void Update()
    {
        if (!m_IsActivated)
            return;

        // Check if all bumpers were hitted, if so, then send points and deactivate
        if (m_HittedComponents.Count == GetToyComponents().Length)
        {
            SendPoints();
            PlayAudioClip();
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

        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                GetToyComponents()[i].OnHit -= OnToyComponentHitted;
                GetToyComponents()[i].SetActive(false);
            }
        }
    }

    public override void ResetToy()
    {
        m_IsActivated = false;
        m_HittedComponents.Clear();

        if (GetToyComponents() != null && GetToyComponents().Length > 0)
        {
            for (int i = 0; i < GetToyComponents().Length; i++)
            {
                GetToyComponents()[i].OnHit -= OnToyComponentHitted;
                GetToyComponents()[i].ResetToyComponent();
            }
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
}