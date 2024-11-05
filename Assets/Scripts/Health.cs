using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float Hp = 100;
    public float MaxHp = 100;
    public Image hpProgressBar;

    IHealthListener healthListener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthListener = GetComponent<IHealthListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hpProgressBar != null)
        {
            hpProgressBar.rectTransform.localScale = new Vector3(Hp / MaxHp, 1, 1);
        }
    }

    public void Damage(int damage)
    {
        Hp -= damage;
        if (healthListener != null)
        {
            healthListener.Hit();
            if (Hp <= 0)
            {
                healthListener.OnDie();
            }
        }
    }

    public interface IHealthListener
    {
        void Hit();
        void OnDie();
    }
}
