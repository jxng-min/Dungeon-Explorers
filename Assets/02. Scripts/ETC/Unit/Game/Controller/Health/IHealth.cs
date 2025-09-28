using UnityEngine;
using System.Collections;
using System;

public interface IHealth
{
    event Action OnDead;

    int HP { get; set; }
    
    bool KnockBack { get; set; }
    bool Dead { get; set; }

    void Initialize(int hp = 0);
    void UpdateHP(int amount);
}