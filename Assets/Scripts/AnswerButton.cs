using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnswerButton : MonoBehaviour
{
    public Text textoDaResposta;
    private AnswerData answerData;
    private GameController gameController;
    void Start(){
    gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {}

    public void Setup(AnswerData data)
    {
        answerData = data;
        textoDaResposta.text = answerData.textoResposta;
    }
    public void HandleClick(){
       gameController.AnswerButtonClicked(answerData.estaCorreta);
    }
}
