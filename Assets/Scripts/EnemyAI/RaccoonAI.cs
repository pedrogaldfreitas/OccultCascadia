using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAI : MonoBehaviour
{
    //INTERFACE ON UNITY
    [Range(0, 50)]
    public int growlRadius = 50;
    [Range(0, 50)]
    public int attackRadius = 50;
    [Range(0, 50)]
    public int minDashRadius = 30;

    public float speed;
    public Vector2 moveDirection;

    private float distanceFromPlayer;
    private float chaseTimer = 0f;

    private float chaseDirectionTimer = 5f; //any value above 1.2f should be fine, just to kickstart the function.

    private Transform parent;
    private Transform landTarget;
    private Transform playerLandTarget;
    private Enemy enemyScript;
    private Rigidbody2D parentRB;
    private SpriteRenderer spriteRenderer;


    public enum State {
        IDLE,
        CHASE,
        CHARGE,
        MAKESPACE,
        JUMPATTACK,
        JUMPATTACKFAST,
        ALERT,
        RELOCATE,
        BACKANDFORTHJUMPTEST
    };
    public State raccoonState;

    private bool CR_running;

    private Vector2 testHopDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CR_running = false;
        moveDirection = new Vector2(0, 0);

        playerLandTarget = GameObject.Find("Player").transform.Find("LandTarget");
        parent = transform.parent;
        parentRB = parent.GetComponent<Rigidbody2D>();
        landTarget = parent.Find("LandTarget");
    }

    // Update is called once per frame
    void Update()
    {
        switch(raccoonState)
        {
            case State.IDLE:
                distanceFromPlayer = Vector2.Distance(playerLandTarget.position, landTarget.position);
                //IDEA: Raccoon is chill when player is far away.
                if (distanceFromPlayer >= growlRadius)
                {
                    //Raccoon is chill when player is far away, eating trash.
                } else if (distanceFromPlayer < growlRadius && distanceFromPlayer >= attackRadius)
                {
                    //Raccoon growls.
                    if (!CR_running)
                    {
                        StartCoroutine(RaccoonBlink());
                    }
                } else
                {
                    raccoonState = State.CHASE;
                    //Raccoon attacks.
                }
                //If player gets closer, Raccoon growls but stays still.

                break;
            case State.CHASE:
                ChangeRaccoonColor("red");
                if (parent.GetComponent<FakeHeightObject>().isGrounded && !enemyScript.movementBlocked)
                {

                    moveDirection = GetChaseDirectionAndSpeedEveryHereAndThere();
                    //moveDirection = (playerLandTarget.position - landTarget.position).normalized;
                    parentRB.MovePosition(parent.position + (Vector3)moveDirection * speed / 15f);

                    if (Vector2.Distance(transform.position, playerLandTarget.transform.position) < 15)
                    {
                        raccoonState = State.JUMPATTACK;
                    }

                    if (Vector2.Distance(landTarget.position, playerLandTarget.position) >= minDashRadius)
                    {
                        MaybeDash();
                    }
                }
                break;
            case State.CHARGE:
                ChangeRaccoonColor("blue");
                if (parent.GetComponent<FakeHeightObject>().isGrounded && !enemyScript.movementBlocked) {
                    moveDirection = (playerLandTarget.position - landTarget.position).normalized;
                    parentRB.MovePosition(parent.position + (Vector3)(moveDirection * speed / 15f * 2.2f));

                    if (Vector2.Distance(transform.position, playerLandTarget.transform.position) < 20f)
                    {
                        raccoonState = State.JUMPATTACKFAST;
                    }
                }
                break;
            case State.JUMPATTACK:
                ChangeRaccoonColor("blue");
                if (parent.GetComponent<FakeHeightObject>().isGrounded && !enemyScript.movementBlocked) {
                    moveDirection = (playerLandTarget.position - landTarget.position).normalized;
                    parent.GetComponent<FakeHeightObject>().Jump(moveDirection * speed * 6f, 40);
                    chaseDirectionTimer = 5f;
                    raccoonState = State.MAKESPACE;
                }
                break;
            case State.JUMPATTACKFAST:
                ChangeRaccoonColor("blue");
                if (parent.GetComponent<FakeHeightObject>().isGrounded && !enemyScript.movementBlocked) {
                    moveDirection = (playerLandTarget.position - landTarget.position).normalized;
                    parent.GetComponent<FakeHeightObject>().Jump(moveDirection * speed * 12f, 32);
                    chaseDirectionTimer = 5f;
                    raccoonState = State.MAKESPACE;
                }
                break;
            case State.MAKESPACE:
                ChangeRaccoonColor("green");
                chaseTimer = 0f;

                if (parent.GetComponent<FakeHeightObject>().isGrounded && !enemyScript.movementBlocked)
                {
                    if (Vector2.Distance(landTarget.position, playerLandTarget.position) <= 30f)
                    {
                        moveDirection = (landTarget.position - playerLandTarget.position).normalized;
                        parentRB.MovePosition(parent.position + (Vector3)(moveDirection * speed * 0.8f / 15f));
                    }
                    else
                    {
                        raccoonState = State.CHASE;
                    }
                }
                break;
            case State.BACKANDFORTHJUMPTEST:
                if (parent.GetComponent<FakeHeightObject>().isGrounded)
                {
                    parent.gameObject.GetComponent<FakeHeightObject>().Jump(testHopDirection * 30f, 40);
                    testHopDirection = -testHopDirection;
                }
                break;
            default:
                break;
        }

    }

    private void MaybeDash()
    {
        chaseTimer += Time.deltaTime;

        // Every 1 second in CHASE, roll for a 1/5 chance to switch to DASH
        if (chaseTimer >= 1f)
        {
            chaseTimer = 0f;

            if (Random.Range(0, 4) == 0) // 1 in 4 chance
            {
                raccoonState = State.CHARGE;
            }
        }
    }

    private Vector2 GetChaseDirectionAndSpeedEveryHereAndThere()
    {
        if (chaseDirectionTimer >= 1.2f)
        {
            chaseDirectionTimer = 0f;
            //Towards player + a perpendicular offset randomly selected within a range.
            //Vector2 directionToPlayer = (playerLandTarget.position - landTarget.position).normalized;

            float offset = Random.Range(-1, 1);
            Vector3 perpendicularDirectionToPlayer = new Vector2(-moveDirection.y, moveDirection.x).normalized * offset;

            return (playerLandTarget.position - landTarget.position + perpendicularDirectionToPlayer).normalized;

            //return (Vector3)(((directionToPlayer * speed) + perpendicularDirectionToPlayer) / 15f);
        } else
        {
            chaseTimer += Time.deltaTime;
            return moveDirection;
        }
    }

    IEnumerator RaccoonBlink()
    {
        CR_running = true;

        Color color1 = new Color(0.6981132f, 0.06915272f, 0.1973518f);
        Color color2 = new Color(1, 0, 0.113f);

        while (true)
        {
            distanceFromPlayer = Vector2.Distance(playerLandTarget.position, landTarget.position);

            if (distanceFromPlayer >= growlRadius || distanceFromPlayer < attackRadius)
            {
                break;
            }

            spriteRenderer.color = spriteRenderer.color == color1 ? color2 : color1;

            yield return new WaitForSeconds(0.25f);
        }

        spriteRenderer.color = color1;
        CR_running = false;
    }

    public void ChangeRaccoonColor(string color)
    {
        Color newColor = new Color(1, 0, 0.113f);
        switch (color)
        {
            case "blue":
                newColor = new Color(0, 0, 1f);
                break;
            case "red":
                newColor = new Color(1, 0, 0);
                break;
            case "green":
                newColor = new Color(0, 1, 0);
                break;
            default:
                newColor = new Color(1, 0, 0.113f);
                break;
        }
        spriteRenderer.color = newColor;
        return;
    }

    private void OnDrawGizmosSelected()
    {
        Transform gizmoCenter = null;

        if (landTarget != null)
        {
            gizmoCenter = landTarget;
        }
        else if (transform.parent != null)
        {
            Transform foundLandTarget = transform.parent.Find("LandTarget");

            if (foundLandTarget != null)
            {
                gizmoCenter = foundLandTarget;
            }
        }

        Vector3 center = gizmoCenter != null ? gizmoCenter.position : transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, growlRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center, minDashRadius);
    }
}
