using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject gems;

    public bool isOpen;

    Animator anim;

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
            if (!isOpen)
            {
                AudioManager.instance.PlaySFX(13);
                UI.instance.anim.SetTrigger("treasure");

                Instantiate(gems, transform.position, transform.rotation);
                anim.SetBool("isOpen", true);

                LevelManager.instance.gemsCollected += 10;
                UI.instance.UpdateGemCount();

                
            }
            isOpen = true;
        }

        
    }
}
