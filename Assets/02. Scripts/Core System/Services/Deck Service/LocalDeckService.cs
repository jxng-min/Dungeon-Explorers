using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;

namespace DeckService
{
    #region Serialization
    [System.Serializable]
    public class DeckData
    {
        public UnitCode[] Data;

        public DeckData()
        {
            Data = new UnitCode[5]
            {
                UnitCode.NICK,
                UnitCode.EMPTY,
                UnitCode.EMPTY,
                UnitCode.EMPTY,
                UnitCode.EMPTY
            };
        }

        public DeckData(UnitCode[] data)
        {
            Data = data;
        }
    }
    #endregion Serialization

    public class LocalDeckService : IDeckService
    {
        private string m_local_data_path;

        private List<UnitCode> m_deck_list;

        public event Action<int, UnitCode, UnitCode> OnUpdatedDeck;

        public UnitCode[] Deck => m_deck_list.ToArray();

        public LocalDeckService()
        {
            m_deck_list = new();

            Load();
        }

        public void Initialize()
        {
            for(int i = 0; i < m_deck_list.Count; i++)
            {
                OnUpdatedDeck?.Invoke(i, UnitCode.EMPTY, m_deck_list[i]);
            }
        }

        public void SetDeck(int index, UnitCode code)
        {
            var legacy_code = m_deck_list[index]; 
            m_deck_list[index] = code;

            OnUpdatedDeck?.Invoke(index, legacy_code, code);
        }

        public bool HasDeck(UnitCode code)
        {
            foreach(var deck in m_deck_list)
            {
                if(deck == code)
                {
                    return true;
                }
            }
            
            return false;
        }

        public int GetIndex(UnitCode code)
        {
            for(int i = 0; i < 5; i++)
            {
                if(m_deck_list[i] == code)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "DeckData.json");

            DeckData deck_data;
            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);

                deck_data = JsonUtility.FromJson<DeckData>(json_data);
                if (deck_data == null)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{m_local_data_path}의 형식에 오류가 있습니다.");
#endif
                    return false;
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 존재하지 않으므로 새로운 덱을 생성합니다.</color>");
#endif
                deck_data = new DeckData();
            }

            for (int i = 0; i < deck_data.Data.Length; i++)
            {
                m_deck_list.Add(deck_data.Data[i]);
            }

            return true;
        }

        public void Save()
        {
            var temp_deck = m_deck_list.ToArray();

            var deck_data = new DeckData(temp_deck);

            var json_data = JsonUtility.ToJson(deck_data, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
    }
}