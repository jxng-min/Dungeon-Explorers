using System.Collections.Generic;
using DeckService;
using InventoryService;
using UnitService;
using UnityEngine;

public class DeckUIInstaller : MonoBehaviour, IInstaller
{
    [Header("덱 뷰")]
    [SerializeField] private DeckView m_deck_view;

    [Header("선택된 슬롯의 목록")]
    [SerializeField] private SelectedDeckSlotView[] m_selected_deck_slot_view_list;

    [Header("덱 선택자 뷰")]
    [SerializeField] private SelectorView m_selector_view;

    private List<SelectedDeckSlotPresenter> m_selected_deck_slot_presenter_list = new();

    public void Install()
    {
        InstallSelectedDeckSlot();
        InstallSelector();
        InstallDeck();
    }

    private void InstallDeck()
    {
        DIContainer.Register<IDeckView>(m_deck_view);

        var deck_presenter = new DeckPresenter(m_deck_view,
                                               DIContainer.Resolve<IUnitDataBase>(),
                                               ServiceLocator.Get<IInventoryService>(),
                                               ServiceLocator.Get<IDeckService>(),
                                               DIContainer.Resolve<SelectorPresenter>());
        DIContainer.Register<DeckPresenter>(deck_presenter);
    }

    private void InstallSelectedDeckSlot()
    {
        for(int i = 0; i < m_selected_deck_slot_view_list.Length; i++)
        {
            var selected_deck_slot_presenter = new SelectedDeckSlotPresenter(m_selected_deck_slot_view_list[i],
                                                                             DIContainer.Resolve<IUnitDataBase>(),
                                                                             ServiceLocator.Get<IDeckService>(),
                                                                             i);
            m_selected_deck_slot_presenter_list.Add(selected_deck_slot_presenter);
        }
    }

    private void InstallSelector()
    {
        DIContainer.Register<ISelectorView>(m_selector_view);

        var selector_presenter = new SelectorPresenter(m_selector_view,
                                                       ServiceLocator.Get<IDeckService>(),
                                                       m_selected_deck_slot_presenter_list.ToArray());
        DIContainer.Register<SelectorPresenter>(selector_presenter);
    }
}
