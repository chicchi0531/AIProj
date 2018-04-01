using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UnknownWord
{
    [SerializeField]
    public string Word = "";

    [SerializeField]
    public string Ruby = "";

    //選択ワードか類似ワードか
    [SerializeField]
    public bool IsChoice = false;

    [SerializeField]
    public string[] SubWord = new string[0];

    //ユーザーの答え
    public string UserAnswer = "";

    //表示品との数
    public int HintCount = 0;
}

public class Game : MonoBehaviour {

    [SerializeField]
    UnknownWord[] _Words = null;
    public UnknownWord[] Words { get { return _Words; } }

    [SerializeField]
    SubText _SubText = null;

    [Header("Prefabs")]
    [SerializeField]
    GameObject[] _PrefabChat = null;

    [SerializeField]
    GameObject _PrefabWord = null;

    [SerializeField]
    GameObject _PrefabHistory = null;

    [Header("Parents")]
    [SerializeField]
    Transform _ChatParent = null;
    [SerializeField]
    Transform _WordParent = null;
    [SerializeField]
    Transform _HistoryParent = null;

    [Header("ScrollBar")]
    [SerializeField]
    ScrollRect _ChatScroll = null;
    [SerializeField]
    ScrollRect _WordScroll = null;
    [SerializeField]
    ScrollRect _HistoryScroll = null;

    [Header("Other")]
    [SerializeField]
    GameObject _Answer = null;
    [SerializeField]
    Image _Back = null;

    //private
    List<int> _WordList = new List<int>();

    //Singleton
    private static Game mInst;
    public static Game GetInstance()
    {
        if (mInst == null)
        {
            GameObject gameObject = Resources.Load("Prefabs/System/Game") as GameObject;
            mInst = Instantiate(gameObject).GetComponent<Game>();
        }
        return mInst;
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (mInst == null)
        {
            this.Setup();
            mInst = this;
        }
        else if (mInst != this)
        {
            Destroy(this.gameObject);
        }
    }

    //初期化処理
    void Setup()
    {

    }

    //レベル遷移
    public void CallTitle()
    {

    }

    public void CallMain()
    {
       
    }

    /// <summary>
    /// チャットメッセージ送信
    /// </summary>
    /// <param name="id"> 話者のID </param>
    /// <param name="mes"> 表示する文字列 </param>
    public void SendChat(int id, string mes)
    {
        var inst = Instantiate(_PrefabChat[id]);
        inst.transform.SetParent(_ChatParent,false);

        var comp = inst.GetComponent<UnitChat>();

        //メッセージセット
        comp.SetMessage(mes);

        //スクロールをリセット
        StartCoroutine(ScrollToBottom(_ChatScroll, _ChatParent.GetComponent<RectTransform>()));

    }

    /// <summary>
    /// 聞き取ったメッセージを表示
    /// </summary>
    /// <param name="mes"> 表示する文字列 </param>
    public void SendHistory(string mes)
    {
        var inst = Instantiate(_PrefabHistory);
        inst.transform.SetParent(_HistoryParent,false);

        var comp = inst.GetComponent<UnitHistory>();

        //メッセージセット
        comp.SetMessage(mes);

        //スクロールをリセット
        StartCoroutine(ScrollToBottom(_HistoryScroll, _HistoryParent.GetComponent<RectTransform>()));
    }

    /// <summary>
    /// 不明ワード送信
    /// </summary>
    /// <param name="id"> 不明ワードのID </param>
    public void SendWord(int id)
    {
        //追加済みなら無視
        if (_WordList.Contains(id)) return;

        var inst = Instantiate(_PrefabWord);
        inst.transform.SetParent(_WordParent,false);

        var comp = inst.GetComponent<UnitUnknownWord>();

        //ワードのセット
        comp.SetWord(id);

        //スクロールをリセット
        StartCoroutine(ScrollToBottom(_WordScroll, _WordParent.GetComponent<RectTransform>()));

        //リストに追加
        _WordList.Add(id);
    }
    
    public void AddHint(int id)
    {
        var game = Game.GetInstance();
        var word = game.Words[id];
        word.HintCount++;
    }

    public void ShowAnswer()
    {
        _Answer.SetActive(true);
    }

    public void ChangeBack()
    {
        StartCoroutine(_ChangeBack(_Back,(_Back.color.a > 0.01f)));
    }
    IEnumerator _ChangeBack(Image img,bool isHide)
    {
        int iv = (isHide) ? 1 : 0;
        int ope = (isHide) ? -1 : 1;
        float progress = 0.0f;
        while(progress < 1.0f)
        {
            progress += 0.05f;
            img.color = new Color(img.color.r, img.color.g, img.color.b, iv + progress * ope);
            yield return null;
        }
    }

    public void PushMessage(string mes, int id)
    {
        var name = "";
        switch(id)
        {
            case 0:name = "エイダ:";break;
            case 1:name = "バベッジ:";break;
            case 2:name = "おじさん:";break;
        }
        _SubText.Push(name + mes);
    }

    IEnumerator ScrollToBottom(ScrollRect rect, RectTransform trans)
    {
        //var pos = rect.verticalNormalizedPosition;
        //var scrollSize = trans.sizeDelta.y / 900.0f;
        //while(pos >= 0.01f)
        //{
        //    rect.verticalNormalizedPosition -= 0.01f / scrollSize;
        //    yield return null;
        //}
        yield return new WaitForSeconds(0.5f);
        rect.verticalNormalizedPosition = 0.0f;

        yield return null;
    }

}
