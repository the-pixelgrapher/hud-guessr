using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class InputFieldSubmit : UnityEvent<string> { }

public class GuessSelectableDelagate : MonoBehaviour
{
    public InputFieldSubmit OnSubmit;

    [SerializeField] private TMP_InputField selectable;

    private void Start()
    {
        selectable.onSubmit.AddListener(val =>
        {
            OnSubmit.Invoke(val);
        });
    }
}
