using InventoryService;
using Units;
using UnityEngine;

public class ShopSlotModel
{
    #region Variables
    private Units.Unit m_unit;
    private IInventoryService m_inventory;
    private IUnitRepository m_unit_repo;
    #endregion Variables

    public void Initialize(IUnitRepository unit_repo, IInventoryService inventory, Units.Unit unit)
    {
        m_unit = unit;
        m_inventory = inventory;
        m_unit_repo = unit_repo;
    }

    public int GetMoney()
    {
        return m_inventory.Money;
    }

    public void UpdateMoney(int amount)
    {
        m_inventory.Money += amount;
    }

    public void AddUnit()
    {
        if (!m_inventory.TryAdd(m_unit.Code))
        {
#if UNITY_EDITOR
            Debug.LogWarning($"<color=red>{m_unit.name}를 획득하는 과정에서 이미 동일한 영웅이 인벤토리에 존재함을 확인했습니다.</color>");
#endif
        }
    }

    public bool HasUnit()
    {
        return m_inventory.HasUnit(m_unit.Code);
    }

    public Hero GetUnit()
    {
        return m_unit as Hero;
    }

    public string GetName(UnitCode code)
    {
        return m_unit_repo.GetName(code);
    }

    public int GetCost()
    {
        return (m_unit as Hero).Price;
    }
}
