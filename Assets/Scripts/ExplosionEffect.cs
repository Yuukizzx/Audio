using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
