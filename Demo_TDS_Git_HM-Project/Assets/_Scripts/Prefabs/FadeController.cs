using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{

    //Fetch the Material from the Renderer of the GameObject
    public Material m_Material;
    public Color initColor;
    public Color FadeColor;
    public bool fading;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(fading)
        {
            m_Material.color = Color.Lerp(initColor, FadeColor, 1f);
        }
        else
        {
            m_Material.color = initColor;
        }
        
    }
}
