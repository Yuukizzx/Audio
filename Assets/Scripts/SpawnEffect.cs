using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public float destroyDelay = 1f; // ��������ʱ��

    void Start()
    {
        Destroy(gameObject, destroyDelay); // ���������������Լ�
    }
}
