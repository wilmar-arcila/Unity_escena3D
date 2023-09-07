using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{   
    [Tooltip("Caja de texto donde se muestra el puntaje del personaje")]
    [SerializeField] private GameObject scoreText_o;
    private TMP_Text scoreText;

    private int score;

    private const int defaultScore = 0;

    void Start()
    {
        // Si existe la clave "PlayerScore" se recupera como puntaje actual, de lo contrario se reinicia el puntaje
        score = PlayerPrefs.HasKey("PlayerScore")?PlayerPrefs.GetInt("PlayerScore"):defaultScore;
        scoreText = scoreText_o.GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }

    public void RaiseScore(int deltaScore){
        score += deltaScore;
        scoreText.text = score.ToString();
    }

    void OnDestroy()    // Método que se ejecuta justo antes de la destrucción del objeto
    {
        PlayerPrefs.SetInt("PlayerScore", score); // Se almacena el puntaje actual con la clave "PlayerScore"
    }
}
