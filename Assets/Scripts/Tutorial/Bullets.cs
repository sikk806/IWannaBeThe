using UnityEngine;

public class Bullets : MonoBehaviour
{
    public Vector2 Velocity = new Vector2(10, 0);

    private void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible || !GameManager.Instance.MainCamera.IsObjectInCameraView(gameObject))
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager GM = GameManager.Instance;
        if (other.gameObject.tag == "Terrain")
        {
            gameObject.SetActive(false);
        }
        if (GM.MainCamera.IsObjectInCameraView(other.gameObject))
        {
            if (other.gameObject.tag == "SavePoint")
            {
                if (other.gameObject)
                {
                    GM.Save();
                }
            }
            else if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<Health>().Damage(1);
                gameObject.SetActive(false);
            }
        }
    }
}
