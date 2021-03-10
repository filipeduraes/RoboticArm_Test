using UnityEngine;
using UnityEngine.SceneManagement;

namespace Miscellaneous
{
    public class ReloadScene : MonoBehaviour
    {
        public void Reload()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }
}
