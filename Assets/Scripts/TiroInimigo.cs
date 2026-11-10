using UnityEngine;

public class TiroInimigo :MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 10f;

    [Header("Animação")]
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float tempoAnimacao = 0.1f;

    private SpriteRenderer sR;
    private int frameAtual;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Inicia a troca de sprites repetida
        InvokeRepeating(nameof(AnimarTiro), tempoAnimacao, tempoAnimacao);
    }

    void Update()
    {
        transform.Translate(Vector2.down * velocidade * Time.deltaTime);

        if (transform.position.y < -5.1f)
        {
            Destroy(gameObject);
        }
    }

    private void AnimarTiro()
    {   
        if (frameAtual >= sprites.Length)
        {
            frameAtual = 0;
        }

        sR.sprite = sprites[frameAtual];
        frameAtual++;
    }
}
