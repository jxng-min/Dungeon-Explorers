using System.Collections.Generic;
using System.IO;
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

    public class LocalDeckSystem : IDeckService
    {
        #region Variables
        private string m_local_data_path;

        private List<UnitCode> m_deck_list;
        #endregion Variables

        public LocalDeckSystem()
        {
            m_deck_list = new();

            Load();
        }

        #region Helper Methods
        public List<UnitCode> GetDeck()
        {
            return m_deck_list;
        }

        public void SetDeck(int index, UnitCode code)
        {
            m_deck_list[index] = code;
        }

        public void Load()
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
                    return;
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
        }

        public void Save()
        {
            var temp_deck = m_deck_list.ToArray();

            var deck_data = new DeckData(temp_deck);

            var json_data = JsonUtility.ToJson(deck_data, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
        #endregion Helper Methods
    }
}