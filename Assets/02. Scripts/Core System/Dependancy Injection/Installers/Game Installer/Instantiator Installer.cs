using DeckService;
using UnitService;
using UnityEngine;

public class InstantiatorInstaller : MonoBehaviour, IInstaller
{
    [Header("유닛 생성자의 목록")]
    [SerializeField] private InstantiatorSlotView[] m_instantiator_view_list;

    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    public void Install()
    {
        InstallUnitDataBase();
        InstallInstantiator();
    }

    private void InstallUnitDataBase()
    {
        DIContainer.Register<IUnitDataBase>(m_unit_db);
    }

    private void InstallInstantiator()
    {
        var deck_service = ServiceLocator.Get<IDeckService>();
        

        for(int index = 0; index < m_instantiator_view_list.Length; index++)
        {
            var unit_code = deck_service.Deck[index];
            var instantiator_presenter = new InstantiatorSlotPresenter(m_instantiator_view_list[index],
                                                                       m_unit_db,
                                                                       unit_code,
                                                                       DIContainer.Resolve<CostPresenter>());
        }
    }
}
