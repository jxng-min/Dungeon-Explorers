using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("적들의 레이어")]
    [SerializeField] private LayerMask m_enemy_layer;
    protected LayerMask Layer
    {
        get => m_enemy_layer;
        set => m_enemy_layer = value;
    }

    private float m_atk;
    protected float ATK
    {
        get => m_atk;
        set => m_atk = value;
    }

    protected virtual void CreateDamageIndicator(Vector3 position)
    {
        var obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        obj.transform.position = position;


        var damage_indicator = obj.GetComponent<DamageIndicator>();
        damage_indicator.Initialize($"<color=#F6BB43>{NumberFormatter.FormatNumber(ATK)}</color>");
    }

    public abstract void Stop();
    public abstract void Resume();

    protected abstract void Return();
}
