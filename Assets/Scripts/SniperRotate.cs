using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRotate : MonoBehaviour
{

    public Transform player;
    public Transform firepoint;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - firepoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(3, -3, 3);
        }
        else
        {
            transform.localScale = new Vector3(3, 3, 3);
        }

        // ÈÃ¾Ñ»÷Ç¹³¯ÏòÍæ¼Ò
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
