using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    Animator animator;

    public int currentHealth, maxHealth, startHealth;

    public float invincibleTime;
    private float invincibleCounter;

    public GameObject deathEffect;

    private SpriteRenderer sr;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = startHealth;

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        UI.instance.UpdateHealthDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        }
        
    }

    public void DealDamage(int damage)
    {
        
        if (invincibleCounter <= 0)
        {
            currentHealth -= damage;

            

            if (currentHealth <= 0)
            {
                Instantiate(deathEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(8);
                currentHealth = 0;

                LevelManager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.6f);

                PlayerController.instance.KnockBack();
            }

            UI.instance.UpdateHealthDisplay();
        }
        
    }

    public void HealPlayer()
    {
        currentHealth += 2;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UI.instance.UpdateHealthDisplay();
    }

        
    
}
