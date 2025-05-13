using TMPro;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private string PeopleName = "Василий";

    private void Start()
    {
        NameText.text= PeopleName;
    }
}
