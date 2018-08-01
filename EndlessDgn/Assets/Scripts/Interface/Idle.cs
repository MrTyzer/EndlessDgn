using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.UI;

public class Idle : InterfaceState
{
    private Camera _camera;
    private Text _stats;
    
    public Idle(State parent, States name, GUIController mainController, Camera camera) : base(parent, name, mainController)
    {
        _camera = camera;
        _stats = _mainController.StatsWindow.GetComponentInChildren<Text>();
    }

    public override void OnEnter()
    {
        Messenger.AddListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnCharacterClick);
    }

    public override void OnEnterExt()
    {
        _mainController.AbilityBar.SetActive(true);
        int i = 0;
        foreach (Ability h in GUIController.CurrentHero.SpellBook)
        {
            _mainController.AbilityButtons[i].gameObject.SetActive(true);
            _mainController.AbilityButtons[i].AbilityShow(h);
            i++;
        }
    }

    public override void OnExit()
    {
        Messenger.RemoveListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnCharacterClick);
    }

    public override void OnExitExt()
    {
        _mainController.AbilityBar.SetActive(false);
    }

    //внутренние методы
    private void OnCharacterClick()
    {
        Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray1, out Hit))
        {
            GameObject hitObject = Hit.transform.gameObject;
            if (hitObject.GetComponent<Creatures>() != null)
            {
                _mainController.SetTarget(hitObject.GetComponent<Creatures>());
                _mainController.StatsWindow.transform.position = Input.mousePosition + new Vector3(180, 0, 0);
                _stats.text = hitObject.GetComponent<Creatures>().ShowStats();
                _mainController.StatsWindow.SetActive(true);
            }
        }
        else
        {
            _mainController.StatsWindow.SetActive(false);
        }
    }

}



