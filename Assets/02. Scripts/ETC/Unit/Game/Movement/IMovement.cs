public interface IMovement
{
    float SPD { get; }
    void Initialize(float speed);
    void Move();
}