using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerWord : MonoBehaviour {

    [SerializeField]
    Text _Word;
    [SerializeField]
    Text _Answer;

    [SerializeField]
    GameObject _Maru;
    [SerializeField]
    GameObject _Peke;

    int _Id = 0;

    // Use this for initialization
    public void Init(int id, Answer controller)
    {
        _Id = id;

        var game = Game.GetInstance();
        var word = game.Words[id];

        _Word.text = word.Ruby;
        _Answer.text = word.UserAnswer;

        if (word.UserAnswer == word.Word)
        {
            _Maru.SetActive(true);
            controller.ResultCount++;
        }
        else
        {
            _Peke.SetActive(true);
        }
    }
}
