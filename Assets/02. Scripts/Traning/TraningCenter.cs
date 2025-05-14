using UnityEngine;

public class TraningCenter : MonoBehaviour
{
    [Header("훈련 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;

    [Header("훈련 슬롯의 프리펩")]
    [SerializeField] private GameObject m_slot_prefab;

    private void OnEnable()
    {
        
    }
}
