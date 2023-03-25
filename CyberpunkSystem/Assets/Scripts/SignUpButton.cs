using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignUpButton : MonoBehaviour {
    [SerializeField]
    private Button _signUpButton;
    private Coroutine _signUpCoroutine;

    private void Reset() {
        _signUpButton = GetComponent<Button>();
    }

    void Start() {
        _signUpButton.onClick.AddListener(HandleRegistrationButtonClicked);
    }

    private void HandleRegistrationButtonClicked() {
        Debug.Log("Click");
        string email = GameObject.Find("InputEmail").GetComponent<TMP_InputField>().text;
        string password = GameObject.Find("InputPassword").GetComponent<TMP_InputField>().text;

        _signUpCoroutine = StartCoroutine(SignUpUser(email, password));
    }

    private IEnumerator SignUpUser(string email, string password) {
        var auth = FirebaseAuth.DefaultInstance;
        var registarTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registarTask.IsCompleted);

        if (registarTask.Exception != null) {
            Debug.LogWarning($"Failed to register task {registarTask.Exception}");
        } else {
            Debug.Log($"Succesfully registered user {registarTask.Result.Email}");

            UserData data = new UserData();

            data.username = GameObject.Find("InputUsername").GetComponent<TMP_InputField>().text;
            string json = JsonUtility.ToJson(data);

            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(registarTask.Result.UserId).SetRawJsonValueAsync(json);
            //Registrar los datos adicionale del usaurio en Database
            //id, como child el amigo
        }
    }
}

public class UserData {
    public int score;
    public string username;
}
