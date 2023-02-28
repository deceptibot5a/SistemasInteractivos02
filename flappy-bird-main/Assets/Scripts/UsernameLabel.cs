using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernameLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    private void Reset() {
        _label = GetComponent<TMP_Text>();
    }

    void Start() {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e) {
        {
            var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

            if (currentUser != null) {
                SetLabelUsername(currentUser.UserId);

                string name = currentUser.DisplayName;
                string email = currentUser.Email;
                Debug.Log("Email:" + email);
            }
        }
    }

    private void SetLabelUsername(string userId) {
        _label.text = userId;
    }
}
