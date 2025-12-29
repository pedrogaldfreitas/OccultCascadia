using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{

    PlayerFacing characterFacingScript;
    Vector3 sidePosition;
    Vector3 topPosition;
    Vector3 bottomPosition;

    Vector3 boxScale;
    Vector3 leftRightScale;
    Vector3 topBottomScale;

    void Start()
    {
        transform.parent.parent.Find("Body").TryGetComponent<PlayerFacing>(out characterFacingScript);
        
        sidePosition = new Vector3(2.5f, 0, 0);
        bottomPosition = new Vector3(0, -1.702f, 0);
        topPosition = new Vector3(0, 1.702f, 0);

        leftRightScale = new Vector3(0.938348f, 0.674026f, 1);
        boxScale = new Vector3(0.938348f, 0.9520616f, 1);
    }

    void Update()
    {
        if (characterFacingScript != null)
        {
            switch(characterFacingScript.playerFacingDir)
            {
                case PlayerFacing.facingDir.LEFT or PlayerFacing.facingDir.RIGHT:
                    if (transform.localPosition != sidePosition)
                    {
                        transform.localPosition = sidePosition;
                        transform.localScale = boxScale;
                    }
                    break;
                case PlayerFacing.facingDir.UP:
                    if (transform.localPosition != topPosition)
                    {
                        transform.localPosition = topPosition;
                        transform.localScale = boxScale;
                    }
                    break;
                case PlayerFacing.facingDir.DOWN:
                    if (transform.localPosition != bottomPosition)
                    {
                        transform.localPosition = bottomPosition;
                        transform.localScale = boxScale;
                    }
                    break;
            }
        }
    }
}
