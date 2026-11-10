using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] float velocidade = 5f;

    [Header("Limites")]
    [SerializeField] const float minX = -4.5f;
    [SerializeField] const float maxX = 4.5f;

    [Header("Tiro")]
    [SerializeField] GameObject prefabTiro;
    [SerializeField] float tProximoTiro = 0.5f, proximoTiro = 0f;
    private bool gameOver;

    private void Update()
    {

        // Limita player dentro da tela
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y);


        #region // Movimento Player
        if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(velocidade * Time.deltaTime * Vector2.right);
        }

        else if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(velocidade * Time.deltaTime * Vector2.left);
        }
        #endregion

        if(Keyboard.current.spaceKey.wasPressedThisFrame && Time.time >= proximoTiro)
        {
            Atirar();
        }
    }

    private void Atirar()
    {
        Instantiate(prefabTiro, transform.position, Quaternion.identity);
        proximoTiro = Time.time + tProximoTiro;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver && (collision.CompareTag("Inimigo") || collision.CompareTag("TiroInimigo")))
        {
            gameOver = true;
            SceneFlow.LoadGameOver();
        }
    }
}