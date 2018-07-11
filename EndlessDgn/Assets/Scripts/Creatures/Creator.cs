using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//переписать
public class Creator : MonoBehaviour
{
    public GameObject HeroPrefab;
    public GameObject SpiderPrefab;
    public GameObject SkeletonPrefab;
    //Дублирование нужно для использования статических методов
    private static GameObject _heroPrefab;
    private static GameObject _spiderPrefab;
    private static GameObject _skeletonPrefab;

    [HideInInspector]
    public static List<Hero> PartyList { get; private set; }


    private void Awake()
    {
        _heroPrefab = HeroPrefab;
        _spiderPrefab = SpiderPrefab;
        _skeletonPrefab = SkeletonPrefab;
        PartyList = new List<Hero>();
        PartyList.Add(CreateHero(HeroPrefab, "Vel"));
        PartyList.Add(CreateHero(HeroPrefab, "Mel"));
        PartyList.Add(CreateHero(HeroPrefab, "Kel"));
    }

    /// <summary>
    /// функция создания персонажей
    /// </summary>
	private static Hero CreateHero(GameObject prefab, string name)//дописать интерфейс создания персонажей
    {
        GameObject hero = Instantiate(prefab);
        hero.GetComponent<Hero>().Name = name;
        return hero.GetComponent<Hero>();
    }

    private static Monsters CreateMonster(GameObject prefab)
    {
        GameObject mob = Instantiate(prefab);
        return mob.GetComponent<Monsters>();
    }

    /// <summary>
    /// функция создания скелета
    /// </summary>
    public static Skeleton CreateSkeleton()
    {
        return (Skeleton)CreateMonster(_skeletonPrefab);
    }

    /// <summary>
    /// функция создания паука
    /// </summary>
    public static Spider CreateSpider()
    {
        return (Spider)CreateMonster(_spiderPrefab);
    }
}
