using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject towerObject;
    [SerializeField]
    private Sprite spriteTower;
    public GameObject TowerObject
    {
        get { return towerObject; }

    }

    public Sprite SpriteTower
    {
        get { return spriteTower; }

    }


}
