using System.Collections.Generic;
using System.Threading;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class AlexKid : MonoBehaviour
{
    [SerializeField]
    float KickSpeed;

    [SerializeField]
    int Hp = 100;

    public ObjectPools BombObjectPool;
    public Grandpa grandpa;

    public bool bStartBossTrigger;
    float checkTime;
    int PatternIndex;
    bool bNextPattern;
    bool bRun;
    bool bBombDrop;
    bool bJump;

    PlayerController Player;
    Vector2 StartPosition = new Vector2(81f, 6.3f);
    List<string> BossPattern = new List<string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        BossPattern.Add("Kick");
        BossPattern.Add("Run");
        BossPattern.Add("BombDrop");
        BossPattern.Add("Jump");

        GetComponent<Animator>().SetTrigger("BombDrop");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        transform.position = StartPosition;
        bStartBossTrigger = false;
        checkTime = 0.0f;
        PatternIndex = 0;
        bRun = false;
        bBombDrop = false;
        bJump = false;
        bNextPattern = true;
        Player = GameManager.Instance.Player;

    }

    // Update is called once per frame
    void Update()
    {
        if (bStartBossTrigger)
        {
            if (bNextPattern == true)
            {
                bNextPattern = false;
                ChangePattern(PatternIndex);
            }
        }

        if (bRun || bBombDrop)
        {
            checkTime += Time.deltaTime;
            if (checkTime > 0.5f)
            {
                checkTime = 0.0f;
                GameObject Bomb = BombObjectPool.GetObject();
                Bomb.transform.position = transform.position;
                Bomb.GetComponent<AlexBomb>().SetForce();
            }
        }

        if (bBombDrop)
        {
            checkTime += Time.deltaTime;
            if (checkTime > 0.35f)
            {
                checkTime = 0.0f;
                GameObject Bomb = BombObjectPool.GetObject();
                Bomb.transform.position = transform.position;
            }
        }

        if (bJump)
        {
            float Speed = KickSpeed / 2;
            if (Player.transform.position.x < transform.position.x)
            {
                if (GetComponent<Rigidbody2D>().linearVelocityX > -Speed)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    GetComponent<Rigidbody2D>().linearVelocityX -= Speed * Time.deltaTime;
                }
            }
            else if (Player.transform.position.x > transform.position.x)
            {
                if (GetComponent<Rigidbody2D>().linearVelocityX < Speed)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    GetComponent<Rigidbody2D>().linearVelocityX += Speed * Time.deltaTime;
                }
            }

            GetComponent<Rigidbody2D>().linearVelocityY -= Time.deltaTime * 12;

            if (transform.position.y < 6.2f)
            {
                GetComponent<Rigidbody2D>().linearVelocityY = 8.0f;
            }
        }
    }

    void ChangePattern(int index)
    {
        GetComponent<Animator>().SetTrigger(BossPattern[index]);
        switch (index)
        {
            case 0:
                PatternKick();
                break;
            case 1:
                PatternRun();
                break;
            case 2:
                PatternBombDrop();
                break;
            case 3:
                PatternJump();
                break;
        }
        PatternIndex++;
        if (PatternIndex > BossPattern.Count - 1)
        {
            PatternIndex = 0;
        }
    }

    void PatternKick()
    {
        transform.position = new Vector2(85.17f, 6.47f);
        GetComponent<Rigidbody2D>().linearVelocityX = -KickSpeed;
        GetComponent<SpriteRenderer>().flipX = true;

        Invoke("ReverseKick", (18 / KickSpeed) + 2);
    }

    void ReverseKick()
    {
        transform.position = new Vector2(62.0f, 6.47f);
        GetComponent<Rigidbody2D>().linearVelocityX = KickSpeed;
        GetComponent<SpriteRenderer>().flipX = false;

        Invoke("PatternBool", (18 / KickSpeed) + 1);
    }

    void PatternRun()
    {
        transform.position = new Vector2(81.17f, 6.15f);
        GetComponent<SpriteRenderer>().flipX = true;
        GetComponent<Rigidbody2D>().linearVelocityX = -4.0f;

        bRun = true;

        Invoke("PatternBool", (18 / 4) + 1);
    }

    void PatternBombDrop()
    {
        transform.position = new Vector2(62.22f, 13.0f);
        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<Rigidbody2D>().linearVelocityX = 4.0f;

        bBombDrop = true;

        Invoke("PatternBool", (18 / 4) + 1);
    }

    void PatternJump()
    {
        transform.position = new Vector2(81.17f, 6.15f);
        GetComponent<SpriteRenderer>().flipX = true;
        GetComponent<Rigidbody2D>().linearVelocityY = 4.0f;

        grandpa.gameObject.SetActive(true);
        grandpa.transform.position = new Vector2(79f, 13.5f);

        bJump = true;
        Invoke("DisappearGrandpa", 15.0f);
    }

    void PatternBool()
    {
        checkTime = 0.0f;
        bRun = false;
        bBombDrop = false;
        bJump = false;
        bNextPattern = true;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    void DisappearGrandpa()
    {
        grandpa.gameObject.SetActive(false);
        bJump = false;
        GetComponent<SpriteRenderer>().flipX = false;
        GetComponent<Rigidbody2D>().linearVelocityX = KickSpeed;
        Invoke("PatternBool", 3.0f);
    }

    public void Hit(int damage)
    {
        Hp -= damage;

    }



}
