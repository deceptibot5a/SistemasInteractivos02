using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWhenUserAuthenticated : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad = "GameScene";

    void Start() {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }

    private void HandleAuthStateChange(object sender, EventArgs e) {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null) {
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}
