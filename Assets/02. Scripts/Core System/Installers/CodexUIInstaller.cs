using UnitService;
using UnityEngine;

public class CodexUIInstaller : MonoBehaviour, IInstaller
{
    [Header("도감 뷰")]
    [SerializeField] private CodexView m_codex_view;

    [Header("컴팩트 도감 뷰")]
    [SerializeField] private CompactCodexView m_compact_codex_view;

    [Header("유닛 데이터베이스")]
    [SerializeField] private UnitDataBase m_unit_db;

    public void Install()
    {
        InstallUnitDataBase();
        InstallCompactCodex();
        InstallCodex();
    }

    private void InstallUnitDataBase()
    {
        DIContainer.Register<IUnitDataBase>(m_unit_db);
    }

    private void InstallCompactCodex()
    {
        DIContainer.Register<ICompactCodexView>(m_compact_codex_view);

        var compact_codex_presenter = new CompactCodexPresenter(m_compact_codex_view,
                                                                ServiceLocator.Get<IUnitService>());
        DIContainer.Register<CompactCodexPresenter>(compact_codex_presenter);
    }

    private void InstallCodex()
    {
        DIContainer.Register<ICodexView>(m_codex_view);

        var codex_presenter = new CodexPresenter(m_codex_view, 
                                                 m_unit_db,
                                                DIContainer.Resolve<CompactCodexPresenter>());
        DIContainer.Register<CodexPresenter>(codex_presenter);
    }
}
