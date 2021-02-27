using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer sr;

    public Sprite cpOn, cpOff;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CheckpointController.instance.DeactivateCheckpoints();

            sr.sprite = cpOn;

            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        sr.sprite = cpOff;
    }
}
