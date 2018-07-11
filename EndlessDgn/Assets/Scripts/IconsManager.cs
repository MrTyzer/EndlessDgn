using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsManager : MonoBehaviour
{
    public Sprite[] Icons;

    [HideInInspector]
    public static Sprite NormalAttackIcon;
    [HideInInspector]
    public static Sprite HeroicStrikeIcon;
    [HideInInspector]
    public static Sprite HealIcon;

    public void Awake()
    {
        NormalAttackIcon = Icons[0];
        HealIcon = Icons[1];
        HeroicStrikeIcon = Icons[2];
    }
}
