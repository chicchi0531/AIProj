using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour {

    [SerializeField]
    Text _Cap = null;
    [SerializeField]
    Text _ResultText = null;

    [SerializeField]
    GameObject _AnswerWordPrefab = null;

    [SerializeField]
    Transform _ContentParent = null;

    public int ResultCount { get; set; }

	// Use this for initialization
	void Start () {
        ResultCount = 0;
        _Cap.text = "";
        _ResultText.text = "";

        StartCoroutine(Result());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Result()
    {
        var game = Game.GetInstance();
        for(int i=0; i<game.Words.Length; i++)
        {
            var inst = Instantiate(_AnswerWordPrefab);
            inst.transform.SetParent(_ContentParent,false);

            var comp = inst.GetComponent<AnswerWord>();
            comp.Init(i,this);

            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.5f);
        _Cap.text = "結果";
        yield return new WaitForSeconds(0.5f);
        switch(ResultCount)
        {
            case 0:
                _ResultText.text = "ポンコツ\nAI";
                break;
            case 1:
                _ResultText.text = "そこそこな\nAI";
                break;
            case 2:
                _ResultText.text = "ちょっと\n人間";
                break;
            case 3:
                _ResultText.text = "なかなか\n人間";
                break;
            case 4:
                _ResultText.text = "かなり\n人間";
                break;
            case 5:
                _ResultText.text = "人間\nソノモノ";
                break;
        }

        yield return null;
    }
}
