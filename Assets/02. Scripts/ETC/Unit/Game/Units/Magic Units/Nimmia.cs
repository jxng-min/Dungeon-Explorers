public class Nimmia : BaseUnit
{
    protected override void Awake()
    {
        base.Awake();
        m_attack = new NimmiaAttack(this);
    }
}
