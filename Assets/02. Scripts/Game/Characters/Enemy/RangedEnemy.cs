using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Enemy", menuName = "Scriptable Object/Create Ranged Enemy")]
public class RangedEnemy : Enemy
{
    [Space(30)]
    [Header("원거리 몬스터가 발사할 탄환 오브젝트의 타입")]
    [SerializeField] ObjectType m_bullet;
    public ObjectType Bullet { get => m_bullet; }
}
