using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        Playing,
        Dead
    }

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
        //DontDestroyOnLoad(gameObject);
        Debug.Log(PlayerPrefs.GetInt("SaveLevel"));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //OriginPosition = transform.position;
        state = State.Playing;

        savePosition = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        Debug.Log(savePosition);
        transform.position = savePosition;

        //LevelInit(PlayerPrefs.GetInt("SaveLevel"));
        LevelInit(testingLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (BottomCollider.IsTouching(TerrainCollider))
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

        grounded = BottomCollider.IsTouching(TerrainCollider);

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
            jumpCnt++;
            vy = JumpSpeed * 0.8f;
        }

        if (Input.GetButtonDown("Jump") && (grounded || (jumpCnt == 0 && !grounded)))
        {
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
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        gameObject.SetActive(true);
        state = State.Playing;
        jumpCnt = 0;
        transform.position = savePosition;
    }

    public void LevelInit(int index)
    {
        Vector2 pos = LevelManager.Instance.PlayerStartLocation[index];
        PlayerPrefs.SetFloat("PositionX", pos.x);
        PlayerPrefs.SetFloat("PositionY", pos.y);
        transform.position = pos;
    }
}
