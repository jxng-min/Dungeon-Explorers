using UnityEngine;

public interface IAttack
{
    int ATK { get; set; }
    float Range { get; set; }
    float Interval { get; set; }

    public void Initialize();
    bool CanAttack();
    Transform GetTarget();
    void CreateDamageIndicator(Transform target_transform);
}