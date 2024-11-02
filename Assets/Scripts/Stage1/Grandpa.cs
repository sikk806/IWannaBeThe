using UnityEngine;

public class Grandpa : MonoBehaviour
{
    bool bAppear;
    private float checkTime = 0f;
    public float attackCycle = 2f;
    public Bomb MonsterBullet;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().speed = 0;
        bAppear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.MainCamera.IsObjectInCameraView(gameObject))
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().SetTrigger("Appear");
            bAppear = true;
        }
        else
        {
            //gameObject.SetActive(false);
            bAppear = false;
        }

        if (bAppear)
        {
            checkTime += Time.deltaTime;
            if (checkTime > attackCycle)
            {
                checkTime = 0;
                GetComponent<Animator>().SetTrigger("Attack");
            }
        }
        else
        {
            checkTime = 2;
        }

    }

    public void ThrowBomb()
    {
        Bomb obj = Instantiate(MonsterBullet);
        obj.transform.position = transform.position;

        obj.GetComponent<Bomb>().SetForce();
    }
}
