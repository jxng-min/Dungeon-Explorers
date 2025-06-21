using System.Collections.Generic;
using UnityEngine;

namespace InventoryService
{
    #region Serialization
    [System.Serializable]
    public class Unit
    {
        public UnitCode Code;
        public int Upgrade;

        public Unit(UnitCode code, int upgrade_count)
        {
            Code = code;
            Upgrade = upgrade_count;
        }        
    }
    #endregion Serialization

    public class LocalInventory : IInventoryService
    {
        #region Variables
        private List<Unit> m_unit_list;
        #endregion Variables

        #region Properties
        public List<Unit> Units { get => m_unit_list; }
        #endregion Properties

        public LocalInventory()
        {
            m_unit_list = new();

            Load();
        }

        #region Helper Methods
        public bool HasUnit(UnitCode code)
        {
            foreach (var unit in m_unit_list)
            {
                if (unit.Code == code)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryAdd(UnitCode code, int upgrade_count = 1)
        {
            if (HasUnit(code))
            {
                return false;
            }

            m_unit_list.Add(new Unit(code, upgrade_count));
            return true;
        }

        public Unit GetUnit(UnitCode code)
        {
            foreach (var unit in m_unit_list)
            {
                if (unit.Code == code)
                {
                    return unit;
                }
            }

            return null;
        }

        public void Load()
        {
            foreach (var unit in DataManager.Instance.Data.Inventory)
            {
                m_unit_list.Add(unit);
            }
        }

        public void Save()
        {
            DataManager.Instance.Data.Inventory = m_unit_list.ToArray();
        }
        #endregion Helper Methods
    }
}