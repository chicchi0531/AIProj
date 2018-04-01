using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubText : MonoBehaviour {
    [SerializeField]
    GameObject _Prefab = null;

    GameObject _First = null;
    GameObject _Second = null;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Push(string mes)
    {
        var inst = Instantiate(_Prefab);
        inst.transform.SetParent(transform,false);
        var comp = inst.GetComponentInChildren<Text>();
        comp.text = mes;

        StartCoroutine(Animate(inst, _First));
    }

    IEnumerator Animate(GameObject first, GameObject second)
    {
        Text[] text = new Text[] {  (_Second)?_Second.GetComponentInChildren<Text>():null,
                                    first.GetComponentInChildren<Text>(),
                                    (second)?second.GetComponentInChildren<Text>():null};
        Image[] img = new Image[] {  (_Second)?_Second.GetComponent<Image>():null,
                                    first.GetComponent<Image>()};
        float progress = 0.0f;


        while(progress<1.0f)
        {
            //二番目だったテキストをフェードアウト
            Color t_color = Color.black;
            Color i_color = Color.white;
            if (_Second)
            {
                t_color = text[0].color;
                t_color.a = 1.0f - progress;
                i_color.a = (1.0f - progress) / 1.5f;
                text[0].color = t_color;
                img[0].color = i_color;
            }

            //二番目のテキストをスライドして、ちょっと暗くする
            if (second)
            {
                var pos = second.transform.position;
                pos.y += 5.0f;
                second.transform.position = pos;

                text[2].color = new Color(1.0f - progress/2.0f,
                                          1.0f - progress/2.0f,
                                          1.0f - progress/2.0f, 1.0f);
            }

            //一番目のテキストをフェードイン            
            t_color = Color.black;
            i_color = Color.white;
            t_color.a = progress;
            i_color.a = progress / 1.5f;
            text[1].color = t_color;
            img[1].color = i_color;


            progress += 0.05f;
            yield return null;
        }

        Destroy(_Second);
        _Second = second;
        _First = first;
    }
}
