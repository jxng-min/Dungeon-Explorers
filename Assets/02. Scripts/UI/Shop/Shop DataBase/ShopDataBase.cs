using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop DataBase", menuName = "SO/DB/Create Shop DataBase")]
public class ShopDataBase : ScriptableObject, IShopDataBase
{
    [Header("상점 데이터 목록")]
    [SerializeField] private ShopData[] m_data_list;

    private Dictionary<UnitCode, ShopData> m_data_dict;

#if UNITY_EDITOR
    private void OnEnable()
    {
        Initialize();
    }
#endif

    private void Initialize()
    {
        if(m_data_list == null)
        {
            return;
        }

        m_data_dict = new();
        foreach(var shop_data in m_data_list)
        {
            m_data_dict.TryAdd(shop_data.Hero.Code, shop_data);
        }
    }

    public int GetCost(UnitCode code)
    {
        if(m_data_dict == null)
        {
            Initialize();
        }

        return m_data_dict.TryGetValue(code, out var shop_data) ? shop_data.Cost : -1;
    }

    public Sprite GetSprite(UnitCode code)
    {
        if(m_data_dict == null)
        {
            Initialize();
        }

        return m_data_dict.TryGetValue(code, out var shop_data) ? shop_data.Hero.Image : null;
    }
}
