using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

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


}
