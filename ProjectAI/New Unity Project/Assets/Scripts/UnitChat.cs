using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitChat: MonoBehaviour {

    [SerializeField]
    Text _Text = null;

    public void SetMessage(string mes)
    {
        _Text.text = mes;
    }
}
