using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;

    public float moveSpeed = 10f;

    private bool levelLoading;

    public LSManager manager;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < .1f && !levelLoading)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                }
            }
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                }
            }
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") > .5f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                }
            }
            if (CrossPlatformInputManager.GetAxisRaw("Vertical") < -.5f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                }
            }

            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUI.instance.ShowInfo(currentPoint);

                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    levelLoading = true;

                    manager.LoadLevel();
                }
            }
        }
    }

        

    void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUI.instance.HideInfo();

        AudioManager.instance.PlaySFX(5);
    }
}
