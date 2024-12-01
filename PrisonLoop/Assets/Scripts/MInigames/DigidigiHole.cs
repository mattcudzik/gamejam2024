using System;
using UnityEngine;

public class DigidigiHole : MiniGameBase
{
    [SerializeField] private Transform hole;
    //0 = Yard
    [SerializeField] public int holeId = 0;
    
    private int myHp;
    private int damage=1;
    private void Start()
    {
        onDigisStarted?.Invoke();
        onMiniGameStart?.Invoke();
        myHp = GameManager.Instance.TunelHp[holeId];
        ChoseTool();
        CalculateSize();
    }
    private void OnDestroy()
    {
        
    }

    private void OnMouseDown()
    {
        if (myHp > 0)
        {
            myHp -= damage;
            GameManager.Instance.TunelHp[holeId] = myHp;
            CalculateSize();
        }
        else
        {
            
            CloseMinigame();
        }

    }

    public void CloseMinigame()
    {
        onMiniGameEnd?.Invoke();
        onDigiClosed?.Invoke();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.G) || Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.Escape))
            {
            CloseMinigame();
            }
    }

    private void CalculateSize()
    {
        float minHp = 0f;
        float maxHp = 1000f;
        float minScale = 0.065f;
        float maxScale = 1f;
        float scale = Mathf.Lerp(maxScale, minScale, Mathf.InverseLerp(minHp, maxHp, myHp));
        hole.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void ChoseTool()
    {
        foreach (var item in GameManager.Instance.PlayerEq.GetItems())
        {
            switch (item.Type)
            {
                case ItemEnum.Shovel:
                {
                    if (damage<5)
                    {
                        damage = 5;
                    }
                    break;
                }
                case ItemEnum.Spoon:
                {
                    if (damage < 2)
                    {
                        damage = 2;
                    }

                    break;
                }
                default:
                {
                    damage = 1;
                    break;
                }
            }
            
        }
    }
}
