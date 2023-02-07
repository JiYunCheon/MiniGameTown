using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private Effect spriteUiEffect = null;
    private Effect spriteEffect = null;

    [SerializeField] private AnimationCurve myAc = null;
    [SerializeField] private Transform canvarsTr = null;

    private void Awake()
    {
        spriteUiEffect = Resources.Load<Effect>("Effect/SpriteUiEffect");
        spriteEffect   = Resources.Load<Effect>("Effect/SpriteEffect");
    }



    public void Inst_SpriteUiEffect(Vector3 pos, string path)
    {
        Effect effect = Instantiate(spriteUiEffect,pos,Quaternion.identity,canvarsTr);
        effect.SetPath(path, effect.spriteAction);
        effect.Run();
    }

    public void Inst_SpriteEffect(Vector3 pos, string path)
    {
        Effect effect = Instantiate(spriteEffect, pos, spriteEffect.transform.rotation);
        effect.SetPath(path, effect.spriteAction);
        effect.Run();
    }









}
