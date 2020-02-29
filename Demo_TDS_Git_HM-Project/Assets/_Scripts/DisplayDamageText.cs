using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayDamageText : MonoBehaviour {

   


    public List<GameObject> DamageTexts;


    public void ShowDText(int Damage)
    {
        
        foreach (GameObject DamageText in DamageTexts)
        {
            if (!DamageText.activeSelf)
            {
                Debug.Log("show damage int !!!!!");
                DamageText.GetComponent<TextMeshProUGUI>().color = Color.white;
                DamageText.GetComponent<DisableTimer>().ResetTimer();
                DamageText.GetComponent<DisableTimer>().Resize(Damage);
                DamageText.GetComponent<TextMeshProUGUI>().text = Damage.ToString();               
                DamageText.SetActive(true);
                return;
            }
            
            

        }
    }
    
}
