using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

namespace Hamish
{
    public class UserInterface : MonoBehaviour
    {
        [Header("Text Boxes")]
        [SerializeField] private TextMeshProUGUI healthNumber;
        [SerializeField] private TextMeshProUGUI[] txtEyes;
        [SerializeField] private TextMeshProUGUI[] txtMouth;
        [SerializeField] private UnityEngine.UI.Image deathImage;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject gameWon;
        private float startingHealth;
        private float curHealth;

        private GameObject playerObject;
        private ent_health playerHealth;

        void Start()
        {
            EventManager.PlayerHasDied += DeathVoid;
            deathImage.enabled = false;
            playerObject = GameObject.FindWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogWarning($"[{name}] Could not find object with the Player tag");
                return;
            }
            else
            {
                Debug.LogWarning($"[{name}] Player's referance is: {playerObject.name}");
            }

            playerHealth = playerObject.GetComponent<ent_health>();
            if (playerHealth == null)
                Debug.LogWarning($"[{name}] PlayerObject doesn't have a health script attatched");
            else
            {
                startingHealth = playerHealth.health;
                curHealth = playerHealth.health;
            }
            TextFace();
            StartCoroutine(EmojiBlink());
        }

        private void DeathVoid()
        {
            deathImage.enabled = true;
            StartCoroutine(CountDown(1));
        }

        private IEnumerator CountDown(int i)
        {
            yield return new WaitForSeconds(5);
            if (i == 1)
                gameOver.SetActive(true);
            else if (i ==2)
                gameWon.SetActive(true);
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        void Update()
        {
            float targetHealth = (playerHealth.health / startingHealth) * 100;
            if (curHealth != targetHealth)
            {
                curHealth -= 4 * Time.deltaTime * (curHealth - targetHealth);
                Mathf.Clamp(curHealth, targetHealth, startingHealth);
            }

            if(curHealth > 0)
                healthNumber.text = curHealth.ToString("#.#") + "%";
            if (curHealth <= 0)
            {
                for (int i = 0; i < txtEyes.Length; i++)
                    txtEyes[i].text = "X  X";
                for (int i = 0; i < txtMouth.Length; i++)
                    txtMouth[i].text = "\n^";

                StopCoroutine(EmojiBlink());
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
            else if (curHealth < 25 && curHealth > 0)
            {
                for (int i = 0; i < txtEyes.Length; i++)
                    txtEyes[i].text = "0  0";
                for (int i = 0; i < txtMouth.Length; i++)
                    txtMouth[i].text = "\n~";
            }
            else if(curHealth <= 0)
            {
                healthNumber.text = "DEAD";
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
}