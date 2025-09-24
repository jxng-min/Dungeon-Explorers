public class Lelia : BaseUnit
{
    protected override void Awake()
    {
        base.Awake();
        m_attack = new LeliaAttack(this);
    }
}
