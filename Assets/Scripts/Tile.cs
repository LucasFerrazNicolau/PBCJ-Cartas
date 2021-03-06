using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false; // Indicador se a carta está revelada ou escondida

    public Sprite originalCarta; // Sprite da frente da carta, utilizado enquanto está revelada
    public Sprite backCarta; // Sprite do verso da carta, utilizado enquanto está escondida

    // Start is called before the first frame update
    void Start()
    {
        EsconderCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Evento disparado pelo clique com botão esquerdo do mouse sobre o Collider do GameObject
    public void OnMouseDown()
    {
        //print("Você pressionou uma Tile!");

        // Aqui não se guardava número de cartas
        //if (tileRevelada)
        //{
        //    EsconderCarta();
        //}
        //else
        //{
        //    RevelarCarta();
        //}

        GameObject.Find("GameManager").GetComponent<ManageCartas>().SelecionarCarta(gameObject);
    }

    /// <summary>
    /// Vira a carta com a face para baixo, escondendo naipe e valor da carta
    /// </summary>
    public void EsconderCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    /// <summary>
    /// Vira a carta com a face para cima, revelando naipe e valor da carta
    /// </summary>
    public void RevelarCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    /// <summary>
    /// Atribui determinado sprite que será utilizado na imagem
    /// </summary>
    /// <param name="novaCarta">Novo sprite</param>
    public void SetOriginalCarta(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
}
