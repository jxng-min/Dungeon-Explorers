using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "New UnitDataBase", menuName = "SO/DB/Create UnitDataBase")]
    public class UnitDataBase : ScriptableObject
    {
        #region Variables
        [Header("유닛 스크립터블 오브젝트의 목록")]
        [SerializeField] private List<Unit> m_unit_list;

        private Dictionary<UnitCode, Unit> m_unit_dictionary;
        #endregion Variables

        #region Properties
        public int Count { get => m_unit_list.Count; }
        #endregion Properties

        private void OnEnable()
        {
            Initialize();
        }

        #region Helper Methods
        private void Initialize()
        {
            m_unit_dictionary = new();

            foreach (var unit in m_unit_list)
            {
                if (!m_unit_dictionary.TryAdd(unit.Code, unit))
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{unit.name}을 추가하는 과정에서 중복된 데이터가 존재했습니다.");
#endif
                }
            }
        }

        public Unit GetUnit(UnitCode code)
        {
            return m_unit_dictionary.TryGetValue(code, out var unit) ? unit : null;
        }
        #endregion Helper Methods
    }
}