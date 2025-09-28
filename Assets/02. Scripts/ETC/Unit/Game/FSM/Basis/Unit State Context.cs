public class UnitStateContext
{
    private readonly BaseUnit m_unit;

    private IState<BaseUnit> m_current_state;

    public UnitStateContext(BaseUnit unit)
    {
        m_unit = unit;
    }

    public void Transition(IState<BaseUnit> state)
    {
        if(m_current_state == state)
        {
            return;
        }

        m_current_state?.ExecuteExit();
        m_current_state = state;
        m_current_state?.ExecuteEnter(m_unit);
    }
}