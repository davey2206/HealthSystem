using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //options
    public bool UseSpriteBased;
    public bool UseBarSmoothing;

    //stats
    //Heart based
    public GameObject Grid;
    public Image HeartImage;
    public Sprite Heart;
    public Sprite EmptyHeart;

    //Bar Based
    public Slider FillSlider;
    public float BarSpeed = 20f;

    private List<GameObject> Sprites = new List<GameObject>();
    private float currentHealth;
    private float maxHealth;
    private float velocity;

    public void Setup(float HP, float maxHP)
    {
        if (UseSpriteBased)
        {
            currentHealth = HP;
            maxHealth = maxHP;
            SpawnHeart();
        }
        else
        {
            FillSlider.maxValue = maxHP;
            FillSlider.value = HP;

            if (UseBarSmoothing)
            {
                currentHealth = HP;
            }
        }
    }

    public void UpdateBar(float damageTaken, float HP, float maxHP)
    {
        if (UseSpriteBased)
        {
            currentHealth = HP;
            maxHealth = maxHP;

            DestroySprites();
            SpawnHeart();
        }
        else
        {
            FillSlider.maxValue = maxHP;

            if (UseBarSmoothing)
            {
                currentHealth = HP;
            }
            else
            {
                FillSlider.value = HP;
            }
        }
    }

    private void DestroySprites()
    {
        foreach (var Sprite in Sprites)
        {
            Destroy(Sprite.gameObject);
        }

        Sprites.Clear();
    }

    private void SpawnHeart()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                HeartImage.sprite = Heart;
            }
            else
            {
                HeartImage.sprite = EmptyHeart;
            }

            GameObject heart = Instantiate(HeartImage.gameObject, Grid.transform);

            Sprites.Add(heart);
        }
    }

    private void Update()
    {
        if (UseBarSmoothing)
        {
            FillSlider.value = Mathf.SmoothDamp(FillSlider.value, currentHealth, ref velocity, BarSpeed * Time.deltaTime);
        }
    }
}
