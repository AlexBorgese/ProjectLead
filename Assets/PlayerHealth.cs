using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
     public Text healthText;
     public int health = 100;
     private float maxAlpha = 0.7f;
     public Image damageFX;
    //Check the effect is active;
    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    void ApplyDamage(int damage)
    {
        health = health - damage;
        UpdateText();
        if (!isActive && damageFX != null) {
            StartCoroutine(SetEffect());
        }
    }

     void ApplyHeal(int heal)
    {
        health = health + heal;
        UpdateText();
    }

     void UpdateText()
    {
        health = Mathf.Clamp(health, 0, 100);
        if (healthText != null) {
            healthText.text = health.ToString();
        }
    }

    void OnCollisionEnter(Collision other)
    {

 //We compare the tag in the other object to the tag name we set earlier.
        if (other.transform.CompareTag("enemyProjectile"))
        {
      
           ApplyDamage(5);
        }
    }

    private IEnumerator SetEffect() {
        isActive = true;
        float alpha = damageFX.color.a;
        Color color = damageFX.color;

        damageFX.color = new Color(color.r, color.g, color.b, maxAlpha);

        yield return new WaitForSeconds(0.2f);

        damageFX.color = new Color(color.r, color.g, color.b, 0);

        yield return new WaitForSeconds(0.4f);
        isActive = false;

        yield return null;
    }


}
