using UnityEngine;

public class CodexSlotPresenter
{
    private readonly ICodexSlotView m_view;
    
    private readonly CompactCodexPresenter m_compact_presenter;
    private readonly Unit m_unit;

    public CodexSlotPresenter(ICodexSlotView view,
                              CompactCodexPresenter compact_presenter,
                              Unit unit)
    {
        m_view = view;

        m_compact_presenter = compact_presenter;
        m_unit = unit;

        m_view.Inject(this);
        Initialize();
    }

    private void Initialize()
    {
        m_view.UpdateUI(m_unit.Image);
    }

    public void OpenCompactUI()
    {
        m_compact_presenter.OpenUI(m_unit);
        m_view.PlaySFX("Button Click");
    }
}
