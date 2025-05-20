using System.Collections.Generic;
using UnityEngine;

public class ExplorerFactory : MonoBehaviour
{
    #region 컴포넌트 관련 필드
    [Header("참조 컴포넌트")]
    [Header("콜러 컨트롤러")]
    [SerializeField] private CallerCtrl m_caller_ctrl;

    [Space(30)]
    [Header("애니메이터 목록")]
    [SerializeField] List<RuntimeAnimatorController> m_animators;

    [Space(30)]
    [Header("오브젝트 생성 위치")]
    [SerializeField] private Transform m_spawn_root;
    #endregion

    private Dictionary<int, RuntimeAnimatorController> m_animator_dict;

    private void Awake()
    {
        m_animators.Sort((arg1, arg2) => arg1.name.CompareTo(arg2.name));

        m_animator_dict = new();
        for (int i = 0; i < m_animators.Count; i++)
        {
            m_animator_dict.Add(i, m_animators[i]);
        }
    }

    public Character Instantiate(int explorer_id)
    {
        Explorer explorer = ExplorerDataManager.Instance.GetExplorer(explorer_id);

        GameObject exp_obj = ObjectManager.Instance.GetObject(ObjectType.EXPLORER);
        exp_obj.transform.position = m_spawn_root.position + Vector3.down * 0.9f;

        var character = exp_obj.GetComponent<Character>();
        if (character)
        {
            Destroy(character);
        }

        switch (explorer.Type)
        {
            case ExplorerType.MELEE:
            case ExplorerType.GUARD:
                character = exp_obj.AddComponent<MeleeCharacter>();
                break;

            case ExplorerType.RANGED:
                character = exp_obj.AddComponent<RangedCharacter>();
                break;

            case ExplorerType.WIZARD:
            {
                switch (explorer_id)
                {
                    case 6:
                        character = exp_obj.AddComponent<Nimmia>();
                        break;

                    case 7:
                        character = exp_obj.AddComponent<Lelia>();
                        break;    
                }    
            }
                break;
        }

        character.Animator.runtimeAnimatorController = GetAnimator(explorer_id);
        character.Script = explorer;
        character.Initialize();

        return character;
    }

    private RuntimeAnimatorController GetAnimator(int explorer_id)
    {
        return m_animator_dict.ContainsKey(explorer_id) ? m_animator_dict[explorer_id] : null;
    }
}
