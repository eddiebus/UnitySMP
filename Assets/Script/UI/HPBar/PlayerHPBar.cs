using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : HPBar
{

    public Animator AnimatorComp;
    protected const string AnimOnDamage = "AnimOnDamage";
    // Start is called before the first frame update
    void Awake()
    {
        HPBarInit();
        AnimatorComp = gameObject.GetComponent<Animator>();
        Player.Get().OnDamage.AddListener(() =>
        {
            if (this.AnimatorComp)
            {
                AnimatorComp.SetTrigger(AnimOnDamage);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        Value = Player.Get().Health;
        _HPBarUpdate();
    }

}
