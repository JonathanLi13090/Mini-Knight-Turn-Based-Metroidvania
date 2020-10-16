using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text PlayerHealthText;

    public void SetPlayerHealth(int playerHealth)
    {
        PlayerHealthText.text = playerHealth.ToString();
    }
}
