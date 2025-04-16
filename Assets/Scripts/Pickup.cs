using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Ammo, SpeedBoost, InfiniteAmmo, Invincibility, Healing }
    public PickupType pickupType;
    public int ammoAmount = 30; // ��ҩ��������������
    public float effectDuration = 5f; // ǿ������ʱ��
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
                Debug.Log("ʰȡ��Ʒ");
                Destroy(gameObject); // ������������Ʒ
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
