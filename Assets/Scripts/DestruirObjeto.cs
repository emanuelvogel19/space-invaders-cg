using UnityEngine;

public class DestruirObjeto : MonoBehaviour
{
    [SerializeField] private float tempoDeVida = 0.27f;

    private void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }
}