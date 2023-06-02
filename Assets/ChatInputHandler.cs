using UnityEngine;
using TMPro;

public class ChatInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public ChatDisplay chatDisplay;

    private void Start()
    {
        //inputField.ActivateInputField(); // Automatically set the focus on the input field when the scene starts
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitMessage(inputField.text);
            inputField.ActivateInputField(); // Reactivate the input field after submitting a message
        }
    }

    public void SubmitMessage(string message)
    {
        // Handle the submitted message here
        message = message.Trim(); // Trim leading and trailing whitespace

        if (!string.IsNullOrEmpty(message))
        {
            chatDisplay.AddMessage(message);
            inputField.text = string.Empty;
            Debug.Log("Submitted message: " + message);
        }
    }
}