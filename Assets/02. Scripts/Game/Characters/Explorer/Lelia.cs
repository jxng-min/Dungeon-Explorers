using UnityEngine;

public class Lelia : WizardCharacter
{
    protected override void Magic()
    {
        if (!m_is_attack)
        {
            return;
        }
        
        var shield_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_SHIELD);

        var holy_shield = shield_obj.GetComponent<HolyShield>();
        holy_shield.Initialize(m_current_atk, m_target.transform.position);
    }
}
