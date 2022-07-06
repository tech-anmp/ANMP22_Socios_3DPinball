using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialFloatPropertyAction : MonoBehaviour, IActionBase
{
    [SerializeField]
    private GameObject m_Object;
    [SerializeField]
    private Material m_Material;
    [SerializeField]
    private string m_Property = "_UseEmissive";
    [SerializeField]
    private float m_DefaultValue = 0;
    [SerializeField]
    private float m_SetValue = 1;

    [SerializeField]
    private bool m_DelayStop;
    [SerializeField]
    private float m_DelayStopTime = 1;

    private Material m_MaterialInstance;

    private void Start()
    {
        MeshRenderer mr = m_Object.GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>();
        mr.GetMaterials(mats);

        for (int i = 0; i < mats.Count; i++)
        {
            string matName = mats[i].name.Replace(" (Instance)", "");
            if(matName == m_Material.name)
            {
                m_MaterialInstance = mats[i];
                m_MaterialInstance.SetFloat(m_Property, m_DefaultValue);
            }
        }
    }

    public void StartAction()
    {
        StartSetProperty();
    }

    public void StopAction()
    {
        if (m_DelayStop)
            StartCoroutine(DelayedStopAction(m_DelayStopTime));
        else
            StopSetProperty();
    }

    private IEnumerator DelayedStopAction(float Time)
    {
        yield return new WaitForSeconds(Time);

        StopSetProperty();
    }

    private void StartSetProperty()
    {
        if (m_MaterialInstance) m_MaterialInstance.SetFloat(m_Property, m_SetValue);
    }

    private void StopSetProperty()
    {
        if (m_MaterialInstance) m_MaterialInstance.SetFloat(m_Property, m_DefaultValue);
    }
}