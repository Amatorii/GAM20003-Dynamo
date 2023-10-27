using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class UserInterface : MonoBehaviour
{
    [Header("Text Boxes")]
    [SerializeField] private TextMeshProUGUI healthNumber;
    [SerializeField] private TextMeshProUGUI[] txtEyes;
    [SerializeField] private TextMeshProUGUI[] txtMouth;
    private float startingHealth;
    private float curHealth;

    public UnityEngine.UI.Image[] imageH;

    private GameObject playerObject;
    private ent_health playerHealth;

    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogWarning("[" + this.name + "] Could not find object with the Player tag");
            return;
        }

        playerHealth = playerObject.GetComponent<ent_health>();
        if (playerHealth == null)
            Debug.LogWarning("[" + this.name + "] PlayerObject doesn't have a health script attatched");
        else
        {
            startingHealth = playerHealth.health;
            curHealth = playerHealth.health;
        }
        TextFace();
        StartCoroutine(EmojiBlink());
    }

    void Update()
    {
        float targetHealth = (playerHealth.health / startingHealth) * 100;
        if (curHealth != targetHealth)
        {
            curHealth -= 4 * Time.deltaTime * (curHealth-targetHealth);
            Mathf.Clamp(curHealth, targetHealth, startingHealth);
        }
        ColourChange();
        healthNumber.text = curHealth.ToString("#.#") + "%";
        if (curHealth <= 0)
        {
            for(int i = 0; i < txtEyes.Length; i++)
                txtEyes[i].text = "X  X";
            for (int i = 0; i < txtMouth.Length; i++)
                txtMouth[i].text = "\n^";



            StopCoroutine(EmojiBlink());
        }
    }

    private void ColourChange()
    {
        Color healthColour = Color.Lerp(Color.red, Color.green, curHealth/75);
        Color imageColour = Color.Lerp(Color.red, Color.white, curHealth/50);

        healthNumber.color = healthColour;

        for(int i = 0; i < imageH.Length; i++)
        {
            imageH[i].color = imageColour;
        }
    }

    private void TextFace()
    {
        if (curHealth > 60)
        {
            for (int i = 0; i < txtEyes.Length; i++)
                txtEyes[i].text = "\u0298  \u0298";
            for (int i = 0; i < txtMouth.Length; i++)
                txtMouth[i].text = "v";
        }
        else if (curHealth <= 60 && curHealth > 25)
        {
            for (int i = 0; i < txtEyes.Length; i++)
                txtEyes[i].text = "o  o";
            for (int i = 0; i < txtMouth.Length; i++)
                txtMouth[i].text = "\n-";
        }
        else if(curHealth <25 &&  curHealth >0)
        {
            for (int i = 0; i < txtEyes.Length; i++)
                txtEyes[i].text = "0  0";
            for (int i = 0; i < txtMouth.Length; i++)
                txtMouth[i].text = "\n~";
        }

    }

    private IEnumerator EmojiBlink()
    {
        while (curHealth > 0)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 2));
            TextFace();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0, 2));
            TextFace();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 2));
            for (int i = 0; i < txtMouth.Length; i++)
                txtEyes[i].text = "_    _";
            yield return new WaitForSeconds(0.1f);
            TextFace();
        }
    }
}
