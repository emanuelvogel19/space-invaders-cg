using UnityEngine;

public class Inimigo : MonoBehaviour
{
    [Header("Animação Inimigo")]
    [SerializeField] Sprite[] sprites;
    [SerializeField]private int frame;
    [SerializeField]private float time = 1f;
    [SerializeField] GameObject explosionPrefab;
    private SpriteRenderer sR;
    private InimigoArea inimigoArea;
    UIManager uIManager;
    public int pontos;
    private bool destruido;

    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {   
        inimigoArea = FindAnyObjectByType<InimigoArea>();
        InvokeRepeating(nameof(AnimarAlien), time,time);
    }

    void AnimarAlien()
    {
        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }
        sR.sprite = sprites[frame];
    }

    public void DestruirInimigo()
    {
        if (destruido)
        {
            return;
        }

        destruido = true;
        FindAnyObjectByType<UIManager>().AtualizarScore(pontos);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        inimigoArea.InimigoMorreu(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destroyer"))
        {
            SceneFlow.LoadGameOver();
        }
    }
}
