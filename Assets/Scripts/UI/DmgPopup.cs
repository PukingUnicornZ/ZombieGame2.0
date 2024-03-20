using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopup : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;
    private float currentTime;
    [SerializeField] private TextMeshProUGUI txt;
    private void Start()
    {
        currentTime = lifeTime;
    }
    void Update()
    {
        if(currentTime < 0)
        {
            Destroy(gameObject);
        }
        currentTime -= Time.deltaTime;
        Color c = txt.color;
        c.a = (1 / lifeTime) * currentTime;
        txt.color = c;
    }
    public void setValue(int v)
    {
        txt.text = "" + v;

    }
}
