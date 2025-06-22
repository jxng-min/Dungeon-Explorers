using System.IO;
using UnityEngine;

namespace UserDataService
{
    public class LocalUserDataSystem : IUserDataService
    {
        #region Variables
        private string m_local_data_path;
        private UserData m_user_data;
        #endregion Variables

        #region Properties
        public UserData UserData
        {
            get => m_user_data;
            set => m_user_data = value;
        }
        #endregion Properties

        public LocalUserDataSystem()
        {
            Load();
        }

        #region Helper Methods
        public void Load()
        {
            m_local_data_path = Path.Combine(Application.persistentDataPath, "UserData.json");

            if (File.Exists(m_local_data_path))
            {
                var json_data = File.ReadAllText(m_local_data_path);
                m_user_data = JsonUtility.FromJson<UserData>(json_data);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log($"<color=green>로컬 경로에 유저 정보가 없어서 새로 생성합니다.</color>");
#endif
                m_user_data = new UserData();
            }
        }

        public void Save()
        {
            var json_data = JsonUtility.ToJson(m_user_data, true);
            File.WriteAllText(m_local_data_path, json_data);
        }
        #endregion Helper Methods
    }
}