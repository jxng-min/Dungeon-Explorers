public class MagicUnit : BaseUnit
{
    protected override void Awake()
    {
        base.Awake();
        m_attack = new MagicAttack(this);
    }
}
