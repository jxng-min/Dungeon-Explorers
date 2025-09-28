public class RangedUnit : BaseUnit
{
    protected override void Awake()
    {
        base.Awake();

        m_attack_state = gameObject.AddComponent<RangedUnitAttackState>();
    }
}