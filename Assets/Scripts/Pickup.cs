using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;

    private bool isCollected;

    public GameObject pickupEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            if (isGem)
            {
                AudioManager.instance.PlaySFX(6);

                LevelManager.instance.gemsCollected++;
                
                isCollected = true;

                Destroy(gameObject);

                GameObject pickup = Instantiate(pickupEffect, transform.position, transform.rotation);

                UI.instance.UpdateGemCount();

                Destroy(pickup, 2f);
            }

            if (isHeal)
            {
                if (PlayerHealth.instance.currentHealth != PlayerHealth.instance.maxHealth)
                {
                    AudioManager.instance.PlaySFX(7);

                    PlayerHealth.instance.HealPlayer();

                    isCollected = true;
                    Destroy(gameObject);

                    GameObject pickup = Instantiate(pickupEffect, transform.position, transform.rotation);
                    Destroy(pickup, 2f);

                    
                }
            }
        }
    }
}
