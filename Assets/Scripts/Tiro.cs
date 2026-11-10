using UnityEngine;
using UnityEngine.InputSystem;
public class Tiro : MonoBehaviour
{

    [Header("Tiro")]
    [SerializeField] float velocidade = 10f;

    void Update()
    {
        transform.Translate(velocidade * Time.deltaTime * Vector2.up);

        if (transform.position.y > 5.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            collision.GetComponent<Inimigo>().DestruirInimigo();
            Destroy(gameObject);
        }
        if (collision.CompareTag("TiroInimigo"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
