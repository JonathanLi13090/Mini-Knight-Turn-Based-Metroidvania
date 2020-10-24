using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIhandler : MonoBehaviour
{
    public Text player_lifes_text;

    public void SetPlayerHealth(int player_lives)
    {
       player_lifes_text.text = player_lives.ToString();
    }
}
