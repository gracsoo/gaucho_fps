using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIPlayer : MonoBehaviour
{
     public GameObject uiPrefab;
    // public Transform target;

    Transform ui;
    Image healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        ui = uiPrefab.transform;
        healthSlider = ui.GetChild(0).GetComponent<Image>();
        GetComponent<CharacterStat>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            // ui.gameObject.SetActive(true);
            // lastMadeVisibleTime = Time.time;

            float healthPercent = currentHealth / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;
            // if(currentHealth <= 0)
            // {
            //     Destroy(ui.gameObject);
            // }
        }
    }

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     if(ui != null)
    //     {
    //         ui.position = target.position;
    //         ui.forward = -cam.forward;

    //         if(Time.time - lastMadeVisibleTime > visibleTime)
    //         {
    //             ui.gameObject.SetActive(false);
    //         }
    //     }
    // }
}
