using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconTarget : ToyComponentBase
{
    [Header("Display")]
    [SerializeField]
    private Material m_MaterialRef;

    protected override void Start()
    {
        base.Start();

        MeshRenderer mr = GetComponent<MeshRenderer>();
        List<Material> mats = new List<Material>();
        mr.GetMaterials(mats);

        for (int i = 0; i < mats.Count; i++)
        {
            string matName = mats[i].name.Replace(" (Instance)", "");
            if (matName == m_MaterialRef.name)
            {
                mr.materials[i].SetInt("_UseEmissive", 0);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider Collision)
    {
        if (!Collision.gameObject.CompareTag(m_CompareTag))
            return;

        if (OnHit != null)
            OnHit(this);

        ChangeIcon();
    }

    public override void ResetToyComponent()
    {
        base.ResetToyComponent();
    }

    private void ChangeIcon()
    {

    }
}
