using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Color Inactive;
    public Color HeartsFilled;
    public Color KeyFilled;

    public GameObject HeartsContainer;
    public Image KeyImage;
    public TMP_Text SecretsText;

    public Player Player;

    private Image[] _hearts;
    
    private int _totalSecrets;

    private void Awake()
    {
        _hearts = HeartsContainer.GetComponentsInChildren<Image>().Where(x => x.gameObject != HeartsContainer.gameObject).ToArray();
        _totalSecrets = FindObjectsOfType<Collectible>(true).Count(x => x.Type == Collectible.CollectibleType.Secret);
    }

    private void Update()
    {
        for (var i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].color = Player.Health > i ? HeartsFilled : Inactive;
        }

        KeyImage.color = Player.HasKey ? KeyFilled : Inactive;

        SecretsText.text = $"{Player.Secrets}/{_totalSecrets}";
    }
}
