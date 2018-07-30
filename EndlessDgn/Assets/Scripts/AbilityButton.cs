using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enums;

public class AbilityButton : MonoBehaviour
{
    public GameObject SelectFrame;
    public GameObject MowingWindow;
    public Image Icon;

    public GUIController GUICountrollerScript;

    private Ability _abilityRef;

    public void AbilityShow(Ability ability)
    {
        _abilityRef = ability;
        switch (ability.Name)
        {
            case Abilities.NormalAttack:
                Icon.sprite = IconsManager.NormalAttackIcon;
                break;
            case Abilities.HeroicStrike:
                Icon.sprite = IconsManager.HeroicStrikeIcon;
                break;
            case Abilities.Heal:
                Icon.sprite = IconsManager.HealIcon;
                break;
            default:
                Icon.sprite = IconsManager.NormalAttackIcon;
                break;
        }
    }

    public void OnPointerEnter()
    {
        MowingWindow.GetComponentInChildren<Text>().text = _abilityRef.AbilityInfoShow();
        MowingWindow.SetActive(true);
        //obj.GetComponent<Button1>()._animator.SetTrigger("EndAnim");
        //obj.GetComponent<Button1>()._animator.SetBool("Attack", false);
        //Button1.weap1.Show();
    }

    public void OnPointerExit()
    {
        MowingWindow.SetActive(false);
        //прячем рамку за край экрана, чтобы она не мешалась
        MowingWindow.transform.position = new Vector3(Screen.width * 2, Screen.height * 2);
    }

    public void OnClick()
    {
        GUIController.SelectionLock = true;
        SelectFrame.transform.position = transform.position;
        SelectFrame.SetActive(true);
        GUICountrollerScript.SetAbility(_abilityRef);
    }

}
