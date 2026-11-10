using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    public TextMeshProUGUI scoreText, highScoreText;
    public int score, highScore;
    private int highScoreSalvo;

    private void Awake()
    {
        highScoreSalvo = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScore = highScoreSalvo;
        AtualizarValorScore();
        AtualizarValorHighScore();
    }
    
    public void AtualizarScore(int pontos)
    {
        score += pontos;
        AtualizarValorScore();

        if (score > highScore)
        {
            highScore = score;
            AtualizarValorHighScore();
        }
    }

    public void FinalizarPartida()
    {
        if (score <= highScoreSalvo)
        {
            return;
        }

        highScoreSalvo = score;
        highScore = highScoreSalvo;
        PlayerPrefs.SetInt(HighScoreKey, highScoreSalvo);
        PlayerPrefs.Save();
        AtualizarValorHighScore();
    }

    private void AtualizarValorScore()
    {
        scoreText.text = score.ToString("0000");
    }

    private void AtualizarValorHighScore()
    {
        highScoreText.text = highScore.ToString("0000");
    }
}
