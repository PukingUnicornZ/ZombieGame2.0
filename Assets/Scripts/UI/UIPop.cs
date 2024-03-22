using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPop : MonoBehaviour
{
    [SerializeField] private float popMax;
    [SerializeField] private float popTime;
    [SerializeField] private bool repeatMode;

    private bool popping;
    private bool reverse;

    RectTransform rect;

    private float timer;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        if (repeatMode)
        {
            popping = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        //Bigger
        if (popping && !reverse)
        {
            if (timer < popTime)
            {
                timer += Time.deltaTime;
                rect.transform.localScale = new Vector3(1 + (popMax / popTime * timer), 1 + (popMax / popTime * timer), 1);
            }
            else
            {
                reverse = true;
            }
        }
        //Smaller
        else if(popping && reverse)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                rect.transform.localScale = new Vector3(1 + (popMax / popTime * timer), 1 + (popMax / popTime * timer), 1);
            }
            else
            {
                //lil reset
                if (!repeatMode) { popping = false; }
                reverse = false;
                rect.transform.localScale = new Vector3(1,1,1);
            }
        }
    }
    //public function for activation
    public void Pop()
    {
        popping = true;
    }
}
