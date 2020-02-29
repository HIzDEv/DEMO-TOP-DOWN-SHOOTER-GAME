using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HM
{
    public class PlayerManager : MonoBehaviour
    {
        #region healthVariables

        public float heathLvl;
        public Image HeathBarre;
        public CameraShake Cshake;
        public DisplayDamageText DDT;
        #endregion

        #region ScoreVaribales
        public int Score=0;
        public int Rank;
        public Image ScroreBarre;
        public Text ScoreTxt;
        #endregion

        #region inventaireVariables
        public int GrenadeInv;
        public int  HealPackInv;
        public Text grenadeTxt, healthPackTxt;


        public  int CurrentAmmo = 30;
        public  int invAmmo = 60;
        public Text Ammotxt,invtxt;
        #endregion

        #region PlayerManagerPanels
        public GameObject LosePanel;
        public GameObject WinPanel;
        #endregion


        private void Awake()
        {
            heathLvl = PLayer_Stats.maxHealth;
            ScroreBarre.fillAmount = 0;
            
        }



        //MAJ les valeurs des UI.text
        public void updateAmmo()
        {
            Ammotxt.text = CurrentAmmo.ToString();
            invtxt.text = invAmmo.ToString();

            grenadeTxt.text = GrenadeInv.ToString();
            healthPackTxt.text = HealPackInv.ToString();
        }


        //ajoute Munitions a l'inventaire
        public void AddAmmo(int value)
        {
            if (invAmmo + value > PLayer_Stats.maxAmmo)
            {
                invAmmo = PLayer_Stats.maxAmmo;
            }
            else
            {
                invAmmo += value;
            }

            updateAmmo();
        }


        //ajoute Grenade et HeathKits a l'inventaire
        public void AddToInventory(int grenade,int heathPack)
        {
            if (grenade + GrenadeInv > PLayer_Stats.MaxGrenadeInv)
            {
                GrenadeInv =PLayer_Stats.MaxGrenadeInv;
            }
            else
            {
                GrenadeInv += grenade;
            }

            if (heathPack+HealPackInv>PLayer_Stats.MaxHealPack)
            {
                HealPackInv = PLayer_Stats.MaxHealPack;
            }
            else
            {
                HealPackInv += heathPack;
            }
                    
            grenadeTxt.text = GrenadeInv.ToString();
            healthPackTxt.text = HealPackInv.ToString();
        }


        //Augmente le score
        public void increseScore(int value)
        {
            Score += value;
            if (Score >= PLayer_Stats.maxRankPoits)
            {
                Rank++;
                Score -= PLayer_Stats.maxRankPoits;
                win();
            }
            ScroreBarre.fillAmount = (float)Score / PLayer_Stats.maxRankPoits;
           
            ScoreTxt.text = Score.ToString();
        }

        //Diminue les points de vie 
        public  void GetHit(float value)
        {
            StartCoroutine(Cshake.Shake());
            heathLvl -= value;
            HeathBarre.fillAmount = heathLvl / PLayer_Stats.maxHealth ;

            DDT.ShowDText((int)value);

            if (heathLvl <= 0)
            {
                PLayer_Stats.Anim.SetTrigger("Die");
            }


        }

        //Soigne le joueur
        public void heal(float value)
        {
            if (heathLvl + value > PLayer_Stats.maxHealth)
            {
                heathLvl = PLayer_Stats.maxHealth;
            }else
            {
                heathLvl += value;
            }
            HeathBarre.fillAmount = heathLvl / PLayer_Stats.maxHealth;

            HealPackInv -= 1;
            updateAmmo();
        }


     //active le Paneau Win
        void win()
        {
            WinPanel.SetActive(true);
        }
        //relance la partie
        public void Replay()
        {
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1;
        }
        //quitter la partie
        public void Quit()
        {
            Application.Quit();
        }
    }
}

