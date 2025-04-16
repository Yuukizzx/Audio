using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Ammo, SpeedBoost, InfiniteAmmo, Invincibility, Healing }
    public PickupType pickupType;
    public int ammoAmount = 30; // 弹药补充量（满弹）
    public float effectDuration = 5f; // 强化持续时间
    public HealthSystem Health;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove player = collision.GetComponent<PlayerMove>();
            HealthSystem Health = collision.GetComponent<HealthSystem>();
            if (player != null)
            {
                ApplyEffect(player);
                Debug.Log("拾取物品");
                Destroy(gameObject); // 触碰后销毁物品
            }
        }
    }

    void ApplyEffect(PlayerMove player)
    {
        switch (pickupType)
        {
            case PickupType.Ammo:
                player.RefillAmmo();
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pickupSound1);
                
                break;
            case PickupType.SpeedBoost:
                player.StartCoroutine(player.SpeedBoost(effectDuration));
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pickupSound2);
                
                break;
            case PickupType.InfiniteAmmo:
                player.InfiniteA();
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pickupSound3);
                break;
            case PickupType.Invincibility:
                player.Invin();
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pickupSound3);
                
                break;
            case PickupType.Healing:
                player.Healing();
                SoundManager.Instance.PlaySFX(SoundManager.Instance.pickupSound4);
                break;
        }
    }
}
