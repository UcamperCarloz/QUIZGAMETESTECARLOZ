using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
       public Text textoPergunta;
        public Text textoPontos;
        public Text textoTimer;
        public Text highscoreText;

        public SimpleObjectPool answerButtonObjectPool;
        public Transform answerButtonParent;
        public GameObject painelDoPerguntas;
        public GameObject painelFimRodada;

        private DataController dataController;
        private RoundData rodadaAtual;
        private QuestionData [] questionPool;

        private bool rodadaAtiva;
        private float tempoRestante;
        private int questionIndex;
        private int playerScore;

        List<int> usedValues = new List<int>();

        private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        rodadaAtual = dataController.GetCurrentRoundData();
        questionPool = rodadaAtual.perguntas;
        tempoRestante = rodadaAtual.limiteDeTempo;

        UpdateTimer();

        playerScore = 0;
        questionIndex = 0;
        ShowQuestion();
        rodadaAtiva = true;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (rodadaAtiva)
        {
            tempoRestante -= Time.deltaTime;
            UpdateTimer();
            if(tempoRestante <= 0)
            {
                EndRound();
            }
        }
    }

    private void UpdateTimer()
    {
        textoTimer.text = "Timer: " + Mathf.Round(tempoRestante).ToString();
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        int random = Random.Range(0, questionPool.Length);
        while (usedValues.Contains(random))
        {
            random = Random.Range(0,questionPool.Length);
        }

        QuestionData questionData = questionPool[random];
        usedValues.Add(random);
        textoPergunta.text = questionData.textoDaPergunta;
        for (int i = 0; i < questionData.respostas.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.respostas[i]);
        }
    }

    private void RemoveAnswerButtons()
    {
        while(answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool estaCorrecto)
{
    if (estaCorrecto)
    {
        playerScore += rodadaAtual.pontosPorAcerto;
        textoPontos.text = "Score: " + playerScore.ToString();
    }

    if(questionPool.Length > questionIndex + 1)
    {
        questionIndex++;
        ShowQuestion();
    }
    else
    {
        EndRound();
    }
}
    public void EndRound()
   {
    rodadaAtiva = false;

    dataController.EnviarNovoHighScore(playerScore);
    highscoreText.text = "High Score: " + dataController.GetHighScore().ToString();

    painelDoPerguntas.SetActive(false);
    painelFimRodada.SetActive(true);
   }

   public void ReturnToMenu()
   {
        SceneManager.LoadScene("Menu");
   }
}