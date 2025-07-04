using System.Collections;
using ObjectPool;
using UnityEngine;

public class NimmiaAttack : MagicAttack
{
    public NimmiaAttack(BaseUnit unit) : base(unit) {}

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
        
        var cross_obj = ObjectManager.Instance.GetObject(ObjectType.HOLY_CROSS);
        cross_obj.transform.position = m_unit.transform.position + Vector3.up * 0.5f + Vector3.right * 0.25f;

        var holy_cross = cross_obj.GetComponent<HolyCross>();
        holy_cross.Initialize(ATK, 8f);
    }
}
