using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class OpenShop : MonoBehaviour
    {
        public void GameView() {  
            SceneManager.LoadScene("GameView");  
        }  
        public void Shopview() {  
            SceneManager.LoadScene("ShopView");  
        }
    }
}
