using UnityEngine;
using System.Collections;

public interface IHealth
{
    float HP { get; }
    bool IsDead { get; }
    Coroutine KnockBackCoroutine { get; set; }

    void Initialize(float hp);
    void UpdateHP(int amount);
    void Death();

    IEnumerator Co_Knockback(Vector2 direction, float amount = 0.4f);
    IEnumerator Co_ReturnUnit(float target_time);
}