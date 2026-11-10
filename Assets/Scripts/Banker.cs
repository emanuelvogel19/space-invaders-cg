using UnityEngine;

public class Banker : MonoBehaviour
{
    public Sprite[] estados;
    int health;
    SpriteRenderer sR;
    Sprite spriteInicial;
    
    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        spriteInicial = sR.sprite;
        Reiniciar();
    }

    public void Reiniciar()
    {
        gameObject.SetActive(true);
        health = estados.Length + 1;
        sR.sprite = spriteInicial;
    }

    private void ReceberDano()
    {
        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        sR.sprite = estados[health - 1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            FindAnyObjectByType<InimigoArea>().InimigoMorreu(collision.gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("TiroInimigo"))
        {
            Destroy(collision.gameObject);
            ReceberDano();
        }

        if(collision.CompareTag("Tiro"))
        {
            Destroy(collision.gameObject);
            ReceberDano();
        }
    }
}
