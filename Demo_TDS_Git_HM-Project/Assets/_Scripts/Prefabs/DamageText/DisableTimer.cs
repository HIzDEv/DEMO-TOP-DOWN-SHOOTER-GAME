using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimer : MonoBehaviour
{
    public float WaitTimer;
    [SerializeField]
    private float Timer;

    public Vector3 NormalScale;
    public Vector3 CriticalScale;


    public void Resize(int Damage)
    {
        if(Damage > 35)
        {
            GetComponent<RectTransform>().localScale = CriticalScale;
        }
        else
        {
            GetComponent<RectTransform>().localScale = NormalScale;
        }
    }

    public void ResetTimer()
    {
        Timer = WaitTimer;
    }


    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActiveRecursively(false);

        }

    }

    
}
