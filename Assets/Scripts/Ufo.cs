using UnityEngine;

public class Ufo : MonoBehaviour
{
    [Header("Ufo")]
    int valorScore;

    private void Awake()
    {
        valorScore = Random.Range(100, 500);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * 2f);

        if (transform.position.x < -5.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tiro"))
        {
            FindAnyObjectByType<UIManager>().AtualizarScore(valorScore);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
