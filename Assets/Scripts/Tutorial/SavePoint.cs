using Unity.VisualScripting;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Spit MonsterBullet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            GetComponent<Animator>().SetTrigger("Save");
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void SetCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void Attack()
    {
        Spit obj = Instantiate(MonsterBullet);
        obj.transform.position = transform.position;

        obj.GetComponent<Spit>().SetForce();
    }
}
