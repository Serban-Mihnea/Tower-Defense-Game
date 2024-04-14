using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TowerManager : Singleton<TowerManager>
{
    private TowerBtn towerBtnSelected;
    [SerializeField]
    private int placingTowersNumber=0;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.tag == "BuiltSite")
                placeTower(hit);   
        }

        if(spriteRenderer.enabled==true)
        {
            followMouse();
        }
        
    }

    private void placeTower(RaycastHit2D hit)
    {
        
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnSelected != null)
        {
            if (placingTowersNumber == 1) {
                hit.collider.tag = "BuiltSiteFull";
                GameObject newTower = Instantiate(towerBtnSelected.TowerObject);
                newTower.transform.position = hit.transform.position;
                placingTowersNumber = 0;
                disableSpriteRenderer();
            }
            
        }
    }


    public void SelectTower(TowerBtn selectedTower)
    {
        towerBtnSelected = selectedTower;
        placingTowersNumber = 1;
        enableSpriteRenderer(towerBtnSelected.SpriteTower);
    }

    private void followMouse()
    {
        transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position=new Vector2(transform.position.x, transform.position.y);
    }

    private void enableSpriteRenderer(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    private void disableSpriteRenderer()
    {
        spriteRenderer.enabled = false;
    }
}
