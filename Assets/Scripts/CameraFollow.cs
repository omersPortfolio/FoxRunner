using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform target;
    public Transform farBackground, 
                     middleBackground;
    public float minHeight, maxHeight;

    public bool stopFollow;

    private Vector2 lastPos;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z); */

        if (!stopFollow)
        {
            if (target == null) return;
            if (middleBackground == null) return;

            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

            //float amountToMoveX = transform.position.x - lastPos.x;
            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .8f;

            lastPos = transform.position;
        }
       
    }
}
