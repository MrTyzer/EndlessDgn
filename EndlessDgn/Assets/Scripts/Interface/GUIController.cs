using UnityEngine;
using System.Collections.Generic;
using StateMachine;

/// <summary>
/// отвечает за отображение информации о ходе боя
/// </summary>
public class GUIController : MonoBehaviour
{
    public BattleInterfaceController MainBattleInterfaceController;
    public GameObject SelectFrame;
    public GameObject AbilityBar;
    public GameObject SelectionCircle;
    public Camera GUICamera;
    public GameObject StatsWindow;
    public AbilityButton[] AbilityButtons;
    
    /// <summary>
    /// машина состояний для интерфейса в бою
    /// </summary>
    private IStateChart _guiStateChart;

    public static RoomType CurrentRoom { get; private set; }
    public static Hero CurrentHero { get; private set; }
    public static Ability SelectedAbility { get; private set; }
    public static Creature SelectedTarget { get; private set; }

    void Awake()
    {
        Messenger.AddListener(GameEvent.ABILITY_BUTTON_CLICK, OnAbilityButtonClick);
        Messenger<RoomType>.AddListener(GameEvent.ROOM_INTERFACE_INIT, OnRoomShow);
        Messenger<Hero>.AddListener(GameEvent.HERO_TURN, OnHeroTurn);
        _guiStateChart = StateChartFactory.GetInterfaceSC(this, MainBattleInterfaceController);
    }

    private void OnRoomShow(RoomType room)
    {
        CurrentRoom = room;
    }

    private void OnHeroTurn(Hero hero)
    {
        CurrentHero = hero;
        AbilityBar.SetActive(true);
        int i = 0;
        foreach (Ability h in CurrentHero.SpellBook)
        {
            AbilityButtons[i].gameObject.SetActive(true);
            AbilityButtons[i].AbilityShow(h);
            i++;
        }
        _guiStateChart.SwitchState(States.Idle);
    }

    private void OnAbilityButtonClick()
    {
        _guiStateChart.SwitchState(States.SelectTarget);
    }

    /// <summary>
    /// установка способности(берется из класса AbilityButton)
    /// </summary>
    public void SetAbility(Ability ability)
    {
        SelectedAbility = ability;
        TurnOnSelectCircles();
    }

    /// <summary>
    /// включает круги выбора таргета под существами, соответствующие условиям используемой способности
    /// </summary>
    public void TurnOnSelectCircles()
    {
        List<Creature> availableTargets = SelectedAbility.GetAvailableTargets(CurrentRoom, CurrentHero);
        foreach (Creature c in availableTargets)
        {
            c.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    /// <summary>
    /// выключает все круги выбора таргета под существами
    /// </summary>
    public void TurnOffAllSelectCircles()
    {
        foreach (Creature c in CurrentRoom.HeroesAndMobs)
        {
            c.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// отменяет активацию способности и сопутствующий ему блок интерфейса SelectionLock
    /// </summary>
    public void DisactivateAbility()
    {
        TurnOffAllSelectCircles();
        SelectedAbility = null;
        SelectedTarget = null;
        SelectFrame.SetActive(false);
    }

    /// <summary>
    /// метод установки таргета
    /// </summary>
    public void SetTarget(Creature target)
    {
        if (target.Alive)
            SelectedTarget = target;

        if (SelectedAbility != null)
            UseAbility();
    }

    /// <summary>
    /// метод использования способности в части GUI
    /// </summary>
    private void UseAbility()
    {
        AbilityBar.SetActive(false);
        _guiStateChart.SwitchState(States.NoInterface);
        SelectFrame.SetActive(false);
        SelectedAbility.UseAbility(SelectedTarget, CurrentHero, CurrentRoom);
        Messenger<GameObject>.Broadcast(GameEvent.ENEMY_HIT, SelectedTarget.gameObject);
        CurrentHero.gameObject.GetComponent<Animator>().SetTrigger("StartAttack");
        DisactivateAbility();
    }
}
