using TMPro;
using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private string peopleName = "Василий";

    private void Start()
    {
        nameText.text= peopleName;
    }
}
