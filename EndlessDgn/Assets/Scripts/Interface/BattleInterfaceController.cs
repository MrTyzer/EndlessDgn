using UnityEngine;
using UnityEngine.UI;

public class BattleInterfaceController : MonoBehaviour
{
    public GameObject StatsWindow;
    public GUIController GUICountrollerScript;

    [SerializeField]
    private Camera _camera;
    private Text _stats;

    private void Awake()
    {
        _stats = StatsWindow.GetComponentInChildren<Text>();
        StatsWindow.SetActive(false);
    }

    /// <summary>
    /// показывает статы
    /// </summary>
    public void ShowStats()
    {
        Ray ray1 = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Physics.Raycast(ray1, out Hit))
        {
            GameObject hitObject = Hit.transform.gameObject;
            if (hitObject.GetComponent<Creatures>() != null)
            {
                GUICountrollerScript.SetTarget(hitObject.GetComponent<Creatures>());
                GUICountrollerScript.StatsWindow.transform.position = Input.mousePosition + new Vector3(180, 0, 0);
                _stats.text = hitObject.GetComponent<Creatures>().ShowStats();
                GUICountrollerScript.StatsWindow.SetActive(true);
            }
        }
        else
        {
            GUICountrollerScript.StatsWindow.SetActive(false);
        }
    }

    public void GetTargetForAbility()
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
}
