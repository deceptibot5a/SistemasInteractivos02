using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour
{
    public void LoadFriends() {
        SceneManager.LoadScene("Friends");
    }
    public void LoadUsersOnline() {
        SceneManager.LoadScene("UsersOnline");
    }
    public void LoadChat() {
        SceneManager.LoadScene("Chat");
    }
}
