public class MeleeUnit : BaseUnit
{
    protected override void Awake()
    {
        base.Awake();
        m_attack = new MeleeAttack(this);
    }
}
