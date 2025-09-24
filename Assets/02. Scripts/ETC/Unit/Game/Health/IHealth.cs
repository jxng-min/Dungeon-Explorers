using UnityEngine;
using System.Collections;
using System;

public interface IHealth
{
    event Action OnDead;

    float HP { get; }
    bool IsDead { get; }
    Coroutine KnockBackCoroutine { get; set; }

    void Initialize(float hp);
    void UpdateHP(int amount);
    void Death();

    IEnumerator Co_Knockback(Vector2 direction, float amount = 0.4f);
    IEnumerator Co_ReturnUnit(float target_time);
}