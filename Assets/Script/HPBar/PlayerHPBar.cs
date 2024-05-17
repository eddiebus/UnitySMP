using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : HPBar
{
    // Start is called before the first frame update
    void Awake()
    {
        HPBarInit();
    }

    // Update is called once per frame
    void Update()
    {
        Value = Player.Get().Health;
        _HPBarUpdate();
    }
}
