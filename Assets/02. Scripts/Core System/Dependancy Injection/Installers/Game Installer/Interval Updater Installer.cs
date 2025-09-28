using UnityEngine;

public class IntervalUpdaterInstaller : MonoBehaviour, IInstaller
{
    [Header("인터벌 뷰")]
    [SerializeField] private IntervalView m_interval_view;

    public void Install()
    {
        InstallInterval();
    }

    private void InstallInterval()
    {
        DIContainer.Register<IIntervalView>(m_interval_view);

        var interval_model = new IntervalModel();
        DIContainer.Register<IntervalModel>(interval_model);

        var interval_presenter = new IntervalPresenter(m_interval_view,
                                                       interval_model);
        DIContainer.Register<IntervalPresenter>(interval_presenter);
    }
}
