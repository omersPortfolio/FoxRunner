using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    public GameObject deathEffect;

    public GameObject collectible;

    [Range(0, 100)]
    public float chanceToDrop;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //other.transform.parent.gameObject.SetActive(false);
            if (other.transform.parent == null) return;

            Destroy(other.transform.parent.gameObject);


            Instantiate(deathEffect, other.transform.position, other.transform.rotation);

            PlayerController.instance.Bounce();
            PlayerController.instance.didJump = false;

            float dropSelect = Random.Range(0, 100f);

            
            if (dropSelect <= chanceToDrop)
            {
                GameObject collectibleClone = Instantiate(collectible, other.transform.position, other.transform.rotation);
                

                Destroy(collectibleClone, 15f);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
