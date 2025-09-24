using System.Collections;
using ObjectPool;
using UnityEngine;

public class LeliaAttack : MagicAttack
{
    public LeliaAttack(BaseUnit unit) : base(unit) {}

    public override IEnumerator Magic(BaseUnit unit, float delay)
    {
        if (!m_unit.Attack.IsAttack)
        {
            yield break;
        }

        float elapsed_time = 0f;

        while (elapsed_time <= delay)
        {
            yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;
            yield return null;
        }
        
        var shield_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_SHIELD);

        var holy_shield = shield_obj.GetComponent<HolyShield>();
        holy_shield.Initialize(ATK, unit.transform.position);
    }
}