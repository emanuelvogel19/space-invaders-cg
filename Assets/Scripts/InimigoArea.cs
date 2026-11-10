using System.Collections.Generic;
using UnityEngine;

public class InimigoArea : MonoBehaviour
{
    [Header("Inimigos")]
    [SerializeField] private Inimigo[] inimigos;
    [SerializeField] private int linhas = 5;
    [SerializeField] private int colunas = 11;
    [SerializeField] private float tiroTimer = 2f;
    const float tempoTiro = 2f;

    [Header("Movimento")]
    Vector2 direcao = Vector2.right;
    [SerializeField] private float velocidade = 0.5f;
    [SerializeField] private float aumentoVelocidadePorInimigo = 0.01f;
    [SerializeField] private float velocidadeMaxima = 2f;
    [SerializeField] private float avancarAoColidirComParede = 0.15f;
    private Vector2 posicaoInicial;
    private Banker[] bankers;
    
    public List<GameObject> todosInimigos = new List<GameObject>();
    public GameObject prefabTiroInimigo;

    public GameObject UfoPrefab;
    Vector2 posicaoUfo = new Vector2(5f, 3.5f);


    private void Start()
    {   
        bankers = FindObjectsByType<Banker>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        InvokeRepeating(nameof(SpawnaUfo), 10f, Random.Range(10f, 20f));
    }

    private void Awake()
    {
        posicaoInicial = transform.position;
        CriarOnda();
    }

    private void CriarOnda()
    {
        transform.position = posicaoInicial;
        direcao = Vector2.right;
        todosInimigos.Clear();

        for(int l = 0; l < linhas; l++)
        {
            for(int c = 0; c < colunas; c++)
            {
                Inimigo inimigo = Instantiate(inimigos[l], transform);

                Vector2 centro = new Vector2( -7.15f, 4.5f);
                Vector2 posicaoLinha = new Vector2(centro.x, centro.y + (l * 0.8f));

                Vector2 posicao = posicaoLinha;
                posicao.x += c * 0.8f;
                inimigo.transform.localPosition = posicao;
                todosInimigos.Add(inimigo.gameObject);
            }
        }
    }

    public void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);

        Vector2 bordaEsquerda = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 bordaDireita = Camera.main.ViewportToWorldPoint(Vector2.right);

        foreach(Transform inimigo in transform)
        {
            if (direcao == Vector2.right && inimigo.position.x >= bordaDireita.x - 0.5f)
            {
                avancaInimigo();
            }
            else if (direcao == Vector2.left && inimigo.position.x <= bordaEsquerda.x + 0.5f)
            {
                avancaInimigo();
            }
        }

        if (todosInimigos.Count > 0 && tiroTimer <= 0f)
        {
            InimigoAtira();
        }
        tiroTimer -= Time.deltaTime;
    }

    void avancaInimigo()
    {
        direcao.x *= -1f;

        Vector2 position = transform.position;
        position.y -= avancarAoColidirComParede;
        transform.position = position;
    }

    private void InimigoAtira()
    {
        Vector2 pos = todosInimigos[Random.Range(0, todosInimigos.Count)].transform.position;
        Instantiate(prefabTiroInimigo, pos, Quaternion.identity);
        tiroTimer = tempoTiro;
    }

    void SpawnaUfo()
    {
        Instantiate(UfoPrefab, posicaoUfo, Quaternion.identity);
    }

    public void InimigoMorreu(GameObject inimigo)
    {
        if (!todosInimigos.Remove(inimigo))
        {
            return;
        }

        velocidade = Mathf.Min(velocidade + aumentoVelocidadePorInimigo, velocidadeMaxima);

        if (todosInimigos.Count == 0)
        {
            FindAnyObjectByType<UIManager>().AtualizarScore(1000);
            foreach (Banker banker in bankers)
            {
                banker.Reiniciar();
            }
            CriarOnda();
        }
    }
}
