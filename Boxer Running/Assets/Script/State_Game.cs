using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class State_Game : MonoBehaviour
{

    public static State_Game state;


 // Winner Variabls
    //public CanvasGroup _win_panel; // which is contaiting all winner items
  //  public Text coinCollected;
    //public Text Diamond_count;
    //public Image Coin_img, Damond_img,key_img;
    //public Image Level_bar_progress;
    //public Text _next_Level;
    //public Image Stars;
    //public Button Skip_button;
    //public Button Reward_more_coins;
    //public Button continue_Button;
    public static int Coin;
    //----->>>Prize Panel<<<-------
    //public Image panel_prize; // which is contaiting all prize items
    //public Image[] Boxes;
    //public Image Get_Keys;





    // Game Over Variabls
    //public CanvasGroup _gameOver_panel; // which is contaiting all game over items
    //public Image _Get_Lives;
    int y = 0;


    private void Start()
    {
        Winning_method();
    }


    public void Winning_method()
    {
        // _win_panel.DOFade(.5f, .3f);
        // PlayerPrefs.SetInt("Coins", Coin);

    }






}
