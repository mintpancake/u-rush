using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float forwardForce;
    public float slideForce;
    public float jumpForce;

    private Rigidbody myBody;
    private Animator myAnimator;
    private bool isGrounded = true;
    private string GROUND_TAG = "Ground";
    private string FINISH_TAG = "FinishLine";
    private string OBSTACLE_TAG = "Obstacle";
    private string JUMP_OVER = "JumpOver";
    private string[] ITEM_TAG = { "Academy+", "Happiness+", "Wealth+", "Health+",
                                  "Academy-", "Happiness-", "Wealth-", "Health-"};
    private float LIMIT_X = 8.2f;
    private float BOOST_DOWN = -0.2f;

    private bool doneFinish = false;
    private bool doJump = false;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator.Play("Unarmed_Run", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.paused)
        {
            return;
        }
        if (GameManager.finish)
        {
            if (!doneFinish)
            {
                StartCoroutine(DoFinish());
                doneFinish = true;
            }
            return;
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            doJump = true;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.paused || GameManager.finish)
        {
            return;
        }
        Vector3 v = myBody.velocity;
        v.z = forwardForce;
        v.x = Input.GetAxis("Horizontal") * slideForce;
        if ((transform.position.x < -LIMIT_X && v.x < 0) || (transform.position.x > LIMIT_X && v.x > 0))
        {
            v.x = 0f;
        }

        if (!isGrounded)
        {
            v.y += BOOST_DOWN;
        }
        if (doJump)
        {
            v.y = jumpForce;
            myBody.velocity = v;
            isGrounded = false;
            myAnimator.Play("Jump", 0);
            doJump = false;
        }
        else
        {
            myBody.velocity = v;
        }

    }


    IEnumerator DoFinish()
    {
        Vector3 v_f = myBody.velocity;
        v_f.x = 0f;
        v_f.z = forwardForce / 2f;
        v_f.y = -1f;
        myBody.velocity = v_f;
        isGrounded = true;
        myAnimator.Play("Unarmed_Walk", 0);
        yield return new WaitForSeconds(2f);
        v_f = myBody.velocity;
        v_f.x = 0f;
        v_f.z = 0f;
        myBody.velocity = v_f;
        myAnimator.Play("Unarmed_Idle2", 0);
        yield return new WaitForSeconds(2f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            if (!GameManager.finish)
            {
                myAnimator.Play("Unarmed_Run", 0);
            }
            Vector3 v = myBody.velocity;
            v.y = 0f;
            myBody.velocity = v;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(OBSTACLE_TAG))
        {
            GameManager.HitObstacle();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[0]))
        {
            GameManager.HitItem(0, true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[1]))
        {
            GameManager.HitItem(1, true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[2]))
        {
            GameManager.HitItem(2, true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[3]))
        {
            GameManager.HitItem(3, true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[4]))
        {
            GameManager.HitItem(0, false);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[5]))
        {
            GameManager.HitItem(1, false);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[6]))
        {
            GameManager.HitItem(2, false);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(ITEM_TAG[7]))
        {
            GameManager.HitItem(3, false);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(FINISH_TAG))
        {
            GameManager.HitFinishLine();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(JUMP_OVER))
        {
            GameManager.JumpOver();
            Destroy(other.gameObject);
        }
    }
}
