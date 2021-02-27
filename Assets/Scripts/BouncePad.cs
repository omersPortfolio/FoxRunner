using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    Animator anim;

    public float bounceForce = 20f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.rb.velocity = new Vector2(PlayerController.instance.rb.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
            PlayerController.instance.animator.SetTrigger("doubleJump");
            AudioManager.instance.PlaySFX(12);
        }
    }
}
