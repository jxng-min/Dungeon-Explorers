using InventoryService;
using UnitService;
using UnityEngine;

public class TrainerUIInstaller : MonoBehaviour, IInstaller
{
    [Header("유닛 강화 뷰")]
    [SerializeField] private TrainerView m_trainer_view;

    [Header("유닛 강화 데이터베이스")]
    [SerializeField] private TrainerDataBase m_trainer_db;

    [Header("컴팩트 유닛 강화 뷰")]
    [SerializeField] private CompactTrainerView m_compact_trainer_view;

    public void Install()
    {
        InstallDataBase();
        InstallCompactTrainer();
        InstallTrainer();
    }

    private void InstallDataBase()
    {
        DIContainer.Register<ITrainerDataBase>(m_trainer_db);
    }

    private void InstallTrainer()
    {
        DIContainer.Register<TrainerView>(m_trainer_view);

        var trainer_presenter = new TrainerPresenter(m_trainer_view,
                                                     m_trainer_db,
                                                     ServiceLocator.Get<IInventoryService>(),
                                                     DIContainer.Resolve<CompactTrainerPresenter>());
        DIContainer.Register<TrainerPresenter>(trainer_presenter);
    }

    private void InstallCompactTrainer()
    {
        DIContainer.Register<ICompactTrainerView>(m_compact_trainer_view);

        var compact_trainer_presenter = new CompactTrainerPresenter(m_compact_trainer_view,
                                                                    ServiceLocator.Get<IInventoryService>(),
                                                                    ServiceLocator.Get<IUnitService>());
        DIContainer.Register<CompactTrainerPresenter>(compact_trainer_presenter);
    }
}
