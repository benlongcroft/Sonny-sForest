using UnityEngine;
using UnityEngine.SceneManagement;

namespace controllers
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
