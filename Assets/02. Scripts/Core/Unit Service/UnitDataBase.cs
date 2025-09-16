using System.Collections.Generic;
using UnityEngine;

namespace UnitService
{
    [CreateAssetMenu(fileName = "New UnitDataBase", menuName = "SO/DB/Create UnitDataBase")]
    public class UnitDataBase : ScriptableObject, IUnitDataBase
    {
        [Header("아군 유닛의 목록")]
        [SerializeField] private List<Unit> m_green_list;

        [Header("적군 유닛의 목록")]
        [SerializeField] private List<Unit> m_red_list;

        private Dictionary<UnitCode, Unit> m_unit_dictionary;

        public List<Unit> GreenList => m_green_list;
        public List<Unit> RedList => m_red_list;

#if UNITY_EDITOR
        private void OnEnable()
        {
            Initialize();
        }
#endif

        private void Initialize()
        {
            if(m_unit_dictionary != null)
            {
                return;
            }
            
            RegisterUnits(m_green_list);
            RegisterUnits(m_red_list);
        }

        private void RegisterUnits(List<Unit> unit_list)
        {
            foreach (var unit in unit_list)
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
            if(m_unit_dictionary == null)
            {
                Initialize();
            }

            return m_unit_dictionary.TryGetValue(code, out var unit) ? unit : null;
        }
    }
}