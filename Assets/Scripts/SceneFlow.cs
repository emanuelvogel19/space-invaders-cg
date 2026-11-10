using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Space-Invaders";
    [SerializeField] private bool gameOverScreen;
    private static bool gameOverLoading;

    private void Awake()
    {
        gameOverLoading = false;
    }

    public static void LoadGameOver()
    {
        if (gameOverLoading)
        {
            return;
        }

        gameOverLoading = true;
        FindAnyObjectByType<UIManager>().FinalizarPartida();
        SceneManager.LoadScene("GameOver");
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnGUI()
    {
        if (gameOverScreen)
        {
            DrawCenteredText("GAME OVER", 0.38f, Mathf.Clamp(Screen.height / 10, 36, 96));
            DrawCenteredText("Pressione Espaço para voltar ao menu", 0.55f, Mathf.Clamp(Screen.height / 28, 20, 40));
            return;
        }

        DrawCenteredText("SPACE INVADERS", 0.34f, Mathf.Clamp(Screen.height / 10, 36, 96));
        DrawCenteredText("Clique Espaço para iniciar o Jogo", 0.53f, Mathf.Clamp(Screen.height / 28, 20, 40));
        DrawCenteredText("© Emanuel Vogel e Eric Dhein", 0.92f, Mathf.Clamp(Screen.height / 45, 14, 24));
    }

    private static void DrawCenteredText(string text, float verticalPosition, int fontSize)
    {
        GUIStyle style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = fontSize,
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white }
        };

        GUI.Label(
            new Rect(0f, Screen.height * verticalPosition - fontSize, Screen.width, fontSize * 2f),
            text,
            style);
    }
}
