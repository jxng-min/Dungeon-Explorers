public interface IState<T>
{
    void ExecuteEnter(T sender);
    void ExecuteExit();
}