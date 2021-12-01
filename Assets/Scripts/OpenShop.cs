using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenShop : MonoBehaviour
{
    public void GameView() {  
        SceneManager.LoadScene("GameView");  
    }  
    public void Shopview() {  
        SceneManager.LoadScene("ShopView");  
    }
}
