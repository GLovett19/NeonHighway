using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //score 
    public Text playerScore;
    //health
    PlayerHealth myHealth;
    public int lastHealth;    
    public Sprite healthPip;
    public Sprite emptyHealthPip;   
    public List<Image> hitPoints;

    //progress tracker 
    SplineWalker myWalker;
    public Slider progressTracker;
    //status effects 
    public List<Image> damageIndicator;
    public float invlunTime;
    float count;
    
    // Start is called before the first frame update
    void Start()
    {
        myHealth = GetComponentInParent<PlayerHealth>();
        myWalker = GetComponentInParent<SplineWalker>();
        lastHealth = myHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        progressTracker.value = myWalker.progress;
        UpdateHealth();
        if (count > 0)
        {
            
            count -= Time.deltaTime;
            Debug.Log("counting");
            foreach (Image indicator in damageIndicator)
            {
                float alpha = (count / invlunTime) * 255;
                indicator.color = new Color32(255, 0, 0, (byte)alpha) * indicator.color;
            }
        }
    }


    public void UpdateHealth()
    {

        if (lastHealth != myHealth.health)
        {
            ShowDamage();
        }
        for (int i = 0; i < hitPoints.Count; i++)
        {
            if (i < myHealth.health)
            {
                hitPoints[i].sprite = healthPip;
            }
            else
            {
                hitPoints[i].sprite = emptyHealthPip;
            }
        }
        lastHealth = myHealth.health;

    }
    public void ShowDamage()
    {
        count = invlunTime;
        foreach (Image indicator in damageIndicator)
        {
            indicator.color = new Color32(0, 0, 0, 255) + Color.red;
        }
    }
}
