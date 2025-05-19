using UnityEngine;

public class Nimmia : WizardCharacter
{
    protected override void Magic()
    {
        if (!m_is_attack)
        {
            return;
        }
        
        var cross_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_CROSS);
        cross_obj.transform.position = transform.position + Vector3.up * 0.5f + Vector3.right * 0.25f;

        var holy_cross = cross_obj.GetComponent<HolyCross>();
        holy_cross.Initialize(m_current_atk, 8f);
    }
}
