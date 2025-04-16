using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public float destroyDelay = 1f; // 动画播放时间

    void Start()
    {
        Destroy(gameObject, destroyDelay); // 动画播放完销毁自己
    }
}
