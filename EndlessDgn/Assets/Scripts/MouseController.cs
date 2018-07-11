using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    public GameObject StatsWindow;

    public GUICountroller GUICountrollerScript;
    [SerializeField]
    private Camera _camera;

    //[serializefield]
    //private gameobject scircle;

    private Text _stats;
    

    private void Awake()
    {
        _stats = StatsWindow.GetComponentInChildren<Text>();
        StatsWindow.SetActive(false);
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0) && !GUICountroller.SelectionLock)
        {
            Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray1, out Hit))
            {
                GameObject hitObject = Hit.transform.gameObject;
                if (hitObject.GetComponent<Creatures>() != null)
                {
                    GUICountrollerScript.SetTarget(hitObject.GetComponent<Creatures>());
                    StatsWindow.transform.position = Input.mousePosition + new Vector3(180, 0, 0);
                    _stats.text = hitObject.GetComponent<Creatures>().ShowStats();
                    StatsWindow.SetActive(true);
                }
                else
                {
                    StatsWindow.SetActive(false);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && GUICountroller.SelectionLock && !GUICountroller.AnimationLock)
        {
            //select target
            Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray1, out Hit))
            {
                GameObject hitObject = Hit.transform.gameObject;
                Creatures target = hitObject.GetComponent<Creatures>();
                if (target != null)
                {
                    if (GUICountroller.SelectedAbility.IsAvailable(GUICountroller.CurrentHero, target))
                        GUICountrollerScript.SetTarget(target);
                    else
                        GUICountrollerScript.DisactivateAbility();
                }
                else if (hitObject.GetComponent<AbilityButton>() == null)
                {
                    GUICountrollerScript.DisactivateAbility();
                }
            }
        }
        //добавить выбор таргета по нажатию клавиши
	}
}
