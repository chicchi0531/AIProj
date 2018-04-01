using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHistory : MonoBehaviour {

    [SerializeField]
    EmojiText _Text = null;

    public void SetMessage(string mes)
    {
        _Text.text = mes;
    }
}
