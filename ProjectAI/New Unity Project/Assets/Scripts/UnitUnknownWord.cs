using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUnknownWord : MonoBehaviour {

    [SerializeField]
    Text _Word;

    [SerializeField]
    Text[] _SubWords;

    [SerializeField]
    GameObject _IconAnalogy = null;
    [SerializeField]
    GameObject _IconChoice = null;

    [SerializeField]
    InputField _Input = null;


    int _Id = 0;


    bool _CoIsRun = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!_CoIsRun) StartCoroutine(UpdateHint());
	}

    IEnumerator UpdateHint()
    {
        _CoIsRun = true;

        var game = Game.GetInstance();
        var word = game.Words[_Id];

        for(int i=0;i<word.HintCount&&i<word.SubWord.Length;i++)
        {
            _SubWords[i].text = word.SubWord[i];
        }

        yield return new WaitForSeconds(0.1f);

        _CoIsRun = false;
        yield return null;
    }

    public void SetWord(int id)
    {
        var game = Game.GetInstance();

        _Id = id;

        var word = game.Words[id];
        _Word.text = word.Ruby;
        _IconAnalogy.SetActive(!word.IsChoice);
        _IconChoice.SetActive(word.IsChoice);
    }


    public void EndInput()
    {
        var game = Game.GetInstance();

        game.Words[_Id].UserAnswer = _Input.text;
    }
}
