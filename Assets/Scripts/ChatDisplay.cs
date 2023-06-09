using UnityEngine;
using TMPro;

public class ChatDisplay : MonoBehaviour
{
    private TMP_Text chatText;

    private void Start()
    {
        chatText = GetComponent<TMP_Text>();
    }

    public void AddMessage(string message)
    {
        string currentText = chatText.text;
        string newText = currentText + message + "\n";
        chatText.text = newText;
    }
}