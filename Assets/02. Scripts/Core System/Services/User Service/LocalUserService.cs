using System;
using System.IO;
using EXPService;
using UnityEngine;

namespace UserService
{
    public class LocalUserService : IUserService
    {
        private string m_local_data_path;
        private IEXPService m_exp_service;

        private UserData m_user_data;

        public event Action<int, int> OnUpdatedLevel;
        public event Action<int> OnUpdatedStage;

        public int LV => m_user_data.LV;
        public int EXP => m_user_data.EXP;
        public int Stage => m_user_data.Stage;

        public LocalUserService()
        {
            m_exp_service = ServiceLocator.Get<IEXPService>();

            Load();
        }

        public void Initialize()
        {
            OnUpdatedLevel?.Invoke(LV, EXP);
            OnUpdatedStage?.Invoke(Stage);
        }

        public void UpdateLevel(int exp)
        {
            m_user_data.EXP += exp;

            while(EXP >= m_exp_service.GetEXP(LV))
            {
                m_user_data.EXP -= m_exp_service.GetEXP(LV);
                m_user_data.LV++;
            }

            OnUpdatedLevel?.Invoke(LV, EXP);
        }

        public void UpdateStage(int stage)
        {
            m_user_data.Stage = stage;

            OnUpdatedStage?.Invoke(Stage);
        }

        public bool Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "UserData.json");

            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);
                m_user_data = JsonUtility.FromJson<UserData>(json_data);

                return true;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>{m_local_data_path}가 없으므로 유저 정보가 없어서 새로 생성합니다.</color>");
#endif
                m_user_data = new UserData();

                return false;
            }
        }

        public void Save()
        {
            var json_data = JsonUtility.ToJson(m_user_data, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
    }
}