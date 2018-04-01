using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScriptReader : MonoBehaviour {

    [SerializeField]
    string _ScenarioPath = "Scenario/000";

    [SerializeField]
    string[] _CharacterName = new string[] { "エイダ", "バベッジ" };

    string[] _Scenarios = null;

    //現在の出力先
    enum PORT { History, Chat };
    PORT _OutPort = PORT.History;

    //現在話している人物
    int _OutId = 0;

	// Use this for initialization
	void Start () {
        var texts = Resources.Load(_ScenarioPath) as TextAsset;

        //コメント文除去
        string str = "";
        for(int i=0; i<texts.text.Length; i++)
        {
            if (texts.text[i] == '\r') continue;

            if(texts.text[i]==';')
            {
                while (texts.text[i] != '\n')
                {
                    if (i >= texts.text.Length) break;
                    i++;
                }
            }
            str += texts.text[i];
        }

        _Scenarios = str.Split('\n');

        Run();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Run()
    {
        StartCoroutine(_Run());
    }

    IEnumerator _Run()
    {
        foreach(var s in _Scenarios)
        {
            if (s.Length == 0) continue;

            //命令文かどうか
            if (s[0] == '#')
            {
                //出力先を設定
                var name = s.Substring(1);
                if (name == _CharacterName[0])
                {
                    _OutId = 0;
                    _OutPort = PORT.Chat;
                }
                else if(name==_CharacterName[1])
                {
                    _OutId = 1;
                    _OutPort = PORT.Chat;
                }
                else
                {
                    _OutPort = PORT.History;
                }
            }
            else if(s[0] == '&')
            {
                //ヒントの表示コマンド
                var id = int.Parse(""+s[1]);

                var game = Game.GetInstance();
                game.AddHint(id);
            }
            else if(s[0] == '%')
            {
                //ウェイトコマンド
                var time = float.Parse(s.Substring(1));
                yield return new WaitForSeconds(time);
            }
            else if(s[0] == '*')
            {
                //システムコマンド
                string command = s.Substring(1);

                switch(command)
                {
                    case "back":
                        Game.GetInstance().ChangeBack();
                        break;
                }

            }
            else
            {
                //@を改行文字に置き換え
                var str = s.Replace('@', '\n');

                //単語を置き換え
                str = SearchWord(str);

                //シナリオ文を出力
                var game = Game.GetInstance();
                
                if(_OutPort == PORT.Chat)
                {
                    game.SendChat(_OutId, str);
                    game.PushMessage(str, _OutId);
                }
                else
                {
                    game.SendHistory(str);
                    game.PushMessage(str, 2);
                }
                
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    //文字列から単語を探して置き換える
    string SearchWord(string str)
    {
        var outstr = "";
        for(int i=0; i<str.Length;i++)
        {
            if(str[i] == '[')
            {
                var num = int.Parse(""+str[++i]);
                var game = Game.GetInstance();
                game.SendWord(num);

                foreach(char c in game.Words[num].Ruby)
                {
                    var tmp = "" + c;
                    outstr += "[" + tmp + "]";
                }
                i++;
            }
            else
                outstr += str[i];
        }

        return outstr;
    }
}
