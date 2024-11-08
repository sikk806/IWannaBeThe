using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Playing,
        Dead
    }

    public List<AudioClip> AudioSourceClip;
    public Collider2D BottomCollider;
    public CompositeCollider2D TerrainCollider;
    public GameObject BloodPrefab;
    public GameObject BulletPrefab;
    public float Speed = 3.0f;
    public float JumpSpeed = 8.0f;

    private Vector2 savePosition;
    public Vector2 SavePosition
    {
        set
        {
            savePosition = value;
        }
    }

    State state;
    AudioSource audioSource;

    float vx = 0, vy = 0;
    float prevVx = 0, prevY = 0;
    bool grounded = true;
    bool JumpCheck = false;
    private int jumpCnt = 0;
    public int JumpCnt
    {
        get
        {
            return jumpCnt;
        }
        set
        {
            jumpCnt = value;
        }
    }

    public int testingLevel = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //OriginPosition = transform.position;
        state = State.Playing;

        savePosition = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        transform.position = savePosition;

        GetComponent<Collider2D>().enabled = false;

        JumpCheck = true;
        Invoke("SetCollision", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (BottomCollider.IsTouching(TerrainCollider) || BottomCollider.IsTouchingLayers(64))
        {
            if (!grounded)
            {
                if (vx == 0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Walk");
                }
            }
            else
            {
                if (vx != prevVx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Walk");
                    }
                }
            }
        }
        else
        {
            if (grounded)
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }
            if (!grounded && transform.position.y < prevY)
            {
                if (jumpCnt == 0)
                {
                    jumpCnt = 1;
                }
            }
        }

        grounded = (BottomCollider.IsTouching(TerrainCollider) || BottomCollider.IsTouchingLayers(64));

        prevY = vy;
        prevVx = vx;
        vy = GetComponent<Rigidbody2D>().linearVelocityY;
        vx = Input.GetAxisRaw("Horizontal") * Speed;

        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (JumpCheck && grounded)
        {
            JumpCheck = false;
            jumpCnt = 0;
        }

        if (Input.GetButtonDown("Jump") && jumpCnt == 1 && !grounded)
        {
            audioSource.PlayOneShot(AudioSourceClip[1]);
            jumpCnt++;
            vy = JumpSpeed * 0.8f;
        }

        if (Input.GetButtonDown("Jump") && (grounded || (jumpCnt == 0 && !grounded)))
        {
            audioSource.PlayOneShot(AudioSourceClip[1]);
            jumpCnt++;
            vy = JumpSpeed;
        }

        if (Input.GetButtonUp("Jump"))
        {
            JumpCheck = true;
            if (vy > 0) vy = 0;
        }

        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);

        if (Input.GetButtonDown("Fire"))
        {
            GetComponent<Animator>().SetTrigger("Attack");
            Vector2 bulletV = new Vector2(15, 0);
            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletV.x = -bulletV.x;
            }

            GameObject bullet = GameManager.Instance.BulletPool.GetObject();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.GetComponent<Bullets>().Velocity = bulletV;
                audioSource.PlayOneShot(AudioSourceClip[0]);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MonsterBullet")
        {

        }
    }

    public void OnDead()
    {
        audioSource.PlayOneShot(AudioSourceClip[2]);
        GameManager.Instance.GameOver.gameObject.SetActive(true);
        GameManager.Instance.AudioManager.ChangeSound(1);
        state = State.Dead;
        gameObject.SetActive(false);
        for (int i = 0; i < GameManager.Instance.BloodPool.GetComponent<ObjectPools>().InitialObjectNumber; i++)
        {
            GameObject blood = GameManager.Instance.BloodPool.GetObject();
            if (blood != null)
            {
                blood.transform.position = transform.position;
                blood.GetComponent<Blood>().SetForce();
            }
        }
    }

    public void ReplayInit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.GameOver.gameObject.SetActive(false);
        GameManager.Instance.AudioManager.ChangeSound(0);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        gameObject.SetActive(true);
        state = State.Playing;
        jumpCnt = 0;
        transform.position = savePosition;
    }

    void SetCollision()
    {
        GetComponent<Collider2D>().enabled = true;
    }
}
