using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta; // Carta a ser descartada

    private bool primeiraCartaSelecionada, segundaCartaSelecionada; // Indicadores para cada carta  escolhida em cada linha
    private GameObject carta1, carta2; // GameObjects das cartas selecionadas
    private string linhaCarta1, linhaCarta2; // Linhas das cartas selecionadas

    bool timerPausado, timerAcionado; // Indicador da ativação do Timer
    float timer; // Variável de tempo

    int numTentativas = 0; // Número de tentativas de escolha de pares na rodada
    int numAcertos = 0; // Número de acerots de pares na rodada

    AudioSource somOk; // Som de acerto

    int ultimoJogo = 0; // Número de jogadas com as quais foi feito o último jogo terminado

    int recorde = 0; // Valor atual do recorde
    string recordista = ""; // Nome do recordista do recorde atual

    // Start is called before the first frame update
    void Start()
    {
        MostrarCartas();
        UpdateTentativas();

        somOk = GetComponent<AudioSource>();

        // Configuração inicial de PlayerPrefs
        //PlayerPrefs.SetInt("jogadas", 9999);
        //PlayerPrefs.SetInt("recorde", 9999);
        //PlayerPrefs.SetString("recordista", "Um Jogador Ruim");

        ultimoJogo = PlayerPrefs.GetInt("jogadas", 0);
        GameObject.Find("Ultima Jogada").GetComponent<Text>().text = "Jogo anterior = " + ultimoJogo;

        recorde = PlayerPrefs.GetInt("recorde", 100);
        GameObject.Find("Recorde").GetComponent<Text>().text = "Recorde = " + recorde;

        recordista = PlayerPrefs.GetString("recordista", "Todas as tentativas");
        GameObject.Find("Recordista").GetComponent<Text>().text = recordista;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            //print(timer);

            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;

                if (carta2.CompareTag(carta1.tag))
                {
                    // Combinação de cartas está correta:

                    Destroy(carta1);
                    Destroy(carta2);

                    numAcertos++;

                    somOk.Play();

                    //if (numAcertos == 13)
                    if (numAcertos == 26)
                    {
                        // Acabaram todas as cartas:

                        PlayerPrefs.SetInt("jogadas", numTentativas);

                        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        if (numTentativas < recorde)
                        {
                            PlayerPrefs.SetInt("recorde", numTentativas);
                            SceneManager.LoadScene("Parabens");
                        }
                        else
                        {
                            SceneManager.LoadScene("PosJogo");
                        }
                    }
                }
                else
                {
                    // Combinação de cartas está errada

                    carta1.GetComponent<Tile>().EsconderCarta();
                    carta2.GetComponent<Tile>().EsconderCarta();
                }

                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";

                timer = 0;
            }
        }
    }

    /// <summary>
    /// Carrega todas as cartas do jogo na cena
    /// </summary>
    void MostrarCartas()
    {
        int[] arrayEmbaralhado = CriarArrayEmbaralhado();
        int[] arrayEmbaralhado2 = CriarArrayEmbaralhado();
        int[] arrayEmbaralhado3 = CriarArrayEmbaralhado();
        int[] arratEmbaralhado4 = CriarArrayEmbaralhado();

        //AddUmaCarta();
        for (int i = 0; i < 13; i++)
        {
            //AddUmaCarta(i);
            //AddUmaCarta(i, arrayEmbaralhado[i]);
            AddUmaCarta(0, i, arrayEmbaralhado[i]);
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);
            AddUmaCarta(2, i, arrayEmbaralhado3[i]);
            AddUmaCarta(3, i, arratEmbaralhado4[i]);
        }
    }

    /// <summary>
    /// Adiciona um novo objeto carta à cena
    /// </summary>
    /// <param name="linha">Linha horizontal do jogo na qual a carta se encontra</param>
    /// <param name="rank">Posição na qual a carta se encontra em sua respectiva linha (0-12)</param>
    /// <param name="valor">Valor da carta no baralho (0-12)</param>
    void AddUmaCarta(int linha, int rank, int valor)
    {
        float escalaOriginalCarta = carta.transform.localScale.x;
        float fatorEscalaX = (600 * escalaOriginalCarta / 110.0f);
        float fatorEscalaY = (945 * escalaOriginalCarta / 110.0f);

        GameObject centro = GameObject.Find("Centro Da Tela");
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank - 13 / 2) * 1.5f,
        //    centro.transform.position.y,
        //    centro.transform.position.z);
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank - 13 / 2) * fatorEscalaX,
        //    centro.transform.position.y,
        //    centro.transform.position.z);
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank - 13 / 2) * fatorEscalaX,
        //    centro.transform.position.y + (linha - 1) * fatorEscalaY,
        //    centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + (rank - 13 / 2) * fatorEscalaX,
            centro.transform.position.y + (linha - 2) * fatorEscalaY,
            centro.transform.position.z);

        //GameObject c = Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //GameObject c = Instantiate(carta, new Vector3(rank * 1.5f, 0, 0), Quaternion.identity);
        GameObject c = Instantiate(carta, novaPosicao, Quaternion.identity);
        c.tag = "" + valor;
        //c.name = "" + valor;
        c.name = "" + linha + "_" + valor;

        string nomeDaCarta = "";

        // Condicional para array ordenado
        //if (rank == 0)
        //    nomeDaCarta = "ace";
        //else if (rank == 10)
        //    nomeDaCarta = "jack";
        //else if (rank == 11)
        //    nomeDaCarta = "queen";
        //else if (rank == 12)
        //    nomeDaCarta = "king";
        //else
        //    nomeDaCarta = "" + (rank + 1);
        //nomeDaCarta = nomeDaCarta + "_of_clubs";

        // Condicional para array embaralhado
        if (valor == 0)
            nomeDaCarta = "ace";
        else if (valor == 10)
            nomeDaCarta = "jack";
        else if (valor == 11)
            nomeDaCarta = "queen";
        else if (valor == 12)
            nomeDaCarta = "king";
        else
            nomeDaCarta = "" + (valor + 1);

        //nomeDaCarta += "_of_clubs";
        //if (linha == 0)
        //    nomeDaCarta += "_of_clubs";
        //else
        //    nomeDaCarta += "_of_spades";
        if (linha == 0)
            nomeDaCarta += "_of_diamonds";
        else if (linha == 1)
            nomeDaCarta += "_of_spades";
        else if (linha == 2)
            nomeDaCarta += "_of_hearts";
        else if (linha == 3)
            nomeDaCarta += "_of_clubs";

        Sprite s1 = Resources.Load<Sprite>(nomeDaCarta);
        //GameObject.Find("" + rank).GetComponent<Tile>().SetOriginalCarta(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().SetOriginalCarta(s1);
    }

    /// <summary>
    /// Cria um array de inteiros embaralhados. Utiliza o algoritmo Fisher-Yates
    /// </summary>
    /// <returns>Array de inteiros embaralhados</returns>
    public int[] CriarArrayEmbaralhado()
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;

        for (int t = 0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        return novoArray;
    }

    /// <summary>
    /// Carrega os valores de uma determinada carta conforme ordem selecionada
    /// </summary>
    /// <param name="carta">Carta selecionada</param>
    public void SelecionarCarta(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;

            carta1.GetComponent<Tile>().RevelarCarta();
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada && carta.name != carta1.name)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;

            carta2.GetComponent<Tile>().RevelarCarta();

            VerificarCartas();
        }
    }

    /// <summary>
    /// Inicia o timer para comparae as duas cartas selecionadas
    /// </summary>
    public void VerificarCartas()
    {
        DispararTimer();

        numTentativas++;
        UpdateTentativas();
    }

    /// <summary>
    /// Ativa o timer de espera de verificação de cartas
    /// </summary>
    public void DispararTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }

    /// <summary>
    /// Atualiza texto da quantidade de tentativas
    /// </summary>
    void UpdateTentativas()
    {
        GameObject.Find("Num Tentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
