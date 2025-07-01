using System.Collections;
using InventoryService;
using UnityEngine;

public interface IAttack
{
    int ATK { get; }
    float ATKCool { get; }
    float Range { get; }
    bool IsAttack { get; }
    Coroutine AttackCoroutine { get; set; }

    void Initialize(int enemy_layer, int atk, float cool_time, float range);
    void Attack();

    IEnumerator Co_Attack(GameObject obj);
    void Action(BaseUnit unit, float delay);
}