using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Armazena novo recordista ou "Você" em caso de preenchimento vazio
    /// </summary>
    public void SaveNewRecordist()
    {
        InputField recordistaInput = GameObject.Find("Recordista").GetComponent<InputField>();

        if (recordistaInput && recordistaInput.text != "")
        {
            PlayerPrefs.SetString("recordista", recordistaInput.text);
        }
        else
        {
            PlayerPrefs.SetString("recordista", "Você");
        }
    }

    /// <summary>
    /// Carrega a cena de pós jogo
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene("PosJogo");
    }

    /// <summary>
    /// Reinicia partida do jogo
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Lab3");
    }

    /// <summary>
    /// Direciona para a cena de créditos
    /// </summary>
    public void ShowCredits()
    {
        SceneManager.LoadScene("Creditos");
    }

    /// <summary>
    /// Finaliza o jogo. Não funciona em Modo de Edição
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}
