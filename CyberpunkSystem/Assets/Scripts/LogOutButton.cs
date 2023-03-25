using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LogOutButton : MonoBehaviour, IPointerClickHandler 
{
    public void OnPointerClick(PointerEventData eventData) {
        FirebaseAuth.DefaultInstance.SignOut();
        FirebaseDatabase.DefaultInstance.RootReference.Child("users-online").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetValueAsync(null);

        SceneManager.LoadScene("Home");
    }
}
