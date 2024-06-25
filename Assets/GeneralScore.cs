using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralScore : MonoBehaviour
{

    public TextMeshProUGUI boxScoreTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        boxScoreTxt.text = "Score: ";
    }

    // Update is called once per frame
    void Update()
    {
        boxScoreTxt.text = "Score: " + GameManager.Instance.GetScore();
    }

}
