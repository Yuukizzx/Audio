using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    void Start()
    {
        // 获取动画播放时间
        float animationLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength); // 动画播放完后销毁
    }
}
