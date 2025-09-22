using DeckService;

public class SelectorPresenter
{
    private readonly ISelectorView m_view;
    private readonly IDeckService m_deck_service;
    private readonly SelectedDeckSlotPresenter[] m_selected_deck_slot_presenters;
    private UnitCode m_unit_code;

    public UnitCode UnitCode => m_unit_code;

    public SelectorPresenter(ISelectorView view,
                             IDeckService deck_service,
                             SelectedDeckSlotPresenter[] selected_deck_slot_presenters)
    {
        m_view = view;
        m_deck_service = deck_service;
        m_selected_deck_slot_presenters = selected_deck_slot_presenters;

        foreach(var selected_deck_slot_presenter in m_selected_deck_slot_presenters)
        {
            selected_deck_slot_presenter.Inject(this);
        }

        m_view.Inject(this);
    }

    public void OpenUI(UnitCode unit_code)
    {
        if(unit_code == UnitCode.EMPTY)
        {
            return;
        }

        m_unit_code = unit_code;
        m_view.OpenUI(m_deck_service.HasDeck(unit_code));
    }

    public void CloseUI()
    {
        m_unit_code = UnitCode.EMPTY;

        HightlightToggle(false);
        m_view.CloseUI();
    }

    public void SetPosition(System.Numerics.Vector2 mouse_postion)
    {
        m_view.SetUIPosition(mouse_postion);
    }

    public void OnClickEnable()
    {
        HightlightToggle(true);
        m_view.ToggleCloseButton(false);
    }

    public void OnClickDisable()
    {
        var index = m_deck_service.GetIndex(m_unit_code);

        if(index != -1)
        {
            m_deck_service.SetDeck(index, UnitCode.EMPTY);
        }

        CloseUI();
    }

    private void HightlightToggle(bool active)
    {
        foreach(var selected_slot_presenter in m_selected_deck_slot_presenters)
        {
            selected_slot_presenter.Hightlight(active);
            m_view.ToggleClose(active);
        }
    }
}
