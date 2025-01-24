using UnityEngine;
using DialogueEditor;

public class ConversationInput : MonoBehaviour
{
    private void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ConversationManager.Instance.PressSelectedOption();
        }
    }
}
