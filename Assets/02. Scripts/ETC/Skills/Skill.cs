using UnityEngine;
using ObjectPool;

public abstract class Skill : MonoBehaviour
{
    [Header("적들의 레이어")]
    [SerializeField] private int m_enemy_layer;
    protected int Layer
    {
        get => m_enemy_layer;
        set => m_enemy_layer = value;
    }

    private int m_atk;
    protected int ATK
    {
        get => m_atk;
        set => m_atk = value;
    }

    protected abstract void Return();

    protected virtual void CreateDamageIndicator(Transform target)
    {
        var indicator_obj = ObjectManager.Instance.GetObject(ObjectType.DAMAGE_INDICATOR);
        indicator_obj.transform.SetParent(target.transform);
        indicator_obj.transform.localPosition = Vector3.up * 0.2f;

        var damage_indicator = indicator_obj.GetComponent<DamageIndicator>();
        damage_indicator.Initialize($"<color=#F6BB43>{NumberFormatter.FormatNumber(ATK)}</color>");
    }
}