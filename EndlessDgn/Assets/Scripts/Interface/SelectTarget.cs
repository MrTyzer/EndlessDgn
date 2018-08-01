using System.Collections.Generic;
using StateMachine;
using UnityEngine;


public class SelectTarget : InterfaceState
{
    private Camera _camera;

    public SelectTarget(State parent, States name, GUIController mainController, Camera camera) : base(parent, name, mainController)
    {
    }

    public override void OnEnter()
    {
        Messenger.AddListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnSelectTarget);
    }

    /// <summary>
    /// метод выбора таргета для способности
    /// </summary>
    private void OnSelectTarget()
    {
        Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray1, out Hit))
        {
            GameObject hitObject = Hit.transform.gameObject;
            Creatures target = hitObject.GetComponent<Creatures>();
            if (target != null)
            {
                if (GUIController.SelectedAbility.IsAvailable(GUIController.CurrentHero, target))
                    _mainController.SetTarget(target);
                else
                    _mainController.DisactivateAbility();
            }
            else if (hitObject.GetComponent<AbilityButton>() == null)
            {
                _mainController.DisactivateAbility();
            }
        }
    }

    public override void OnEnterExt()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        Messenger.RemoveListener(GameEvent.ON_LEFT_MOUSE_BUTTON_DOWN, OnSelectTarget);
    }

    public override void OnExitExt()
    {
        throw new System.NotImplementedException();
    }
}
