using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;

    void Update()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text=score.ToString();
    }
}
