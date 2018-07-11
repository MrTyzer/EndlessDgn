using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UICursor : MonoBehaviour
{
    [SerializeField]
    private GameObject Text1;

    void Awake()
    {
        Text1.SetActive(false);
    }

    //public void OnPointerEnter(GameObject obj)
    //{
    //    Text1.GetComponentInChildren<Text>().text = obj.GetComponent<Button1>().weap1.Show();
    //    Text1.SetActive(true);
    //    obj.GetComponent<Button1>()._animator.SetTrigger("EndAnim");
    //    //obj.GetComponent<Button1>()._animator.SetBool("Attack", false);
    //    //Button1.weap1.Show();
    //}

    //public void OnPointerExit(GameObject obj)
    //{
    //    Text1.SetActive(false);
    //    //прячем рамку за край экрана, чтобы она не мешалась
    //    Text1.transform.position = new Vector3(Screen.width * 2, Screen.height * 2);
    //}

    

}
