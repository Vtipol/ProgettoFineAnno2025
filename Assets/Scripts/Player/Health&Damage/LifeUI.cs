using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private Sprite _heartSprite;
    [SerializeField] private Sprite _brokenHeartSprite;

    [SerializeField] private Transform _lifeContainer;


    [Tooltip("Just for testing")]
    [SerializeField] private int _maxHearts;

    private int _hearts;

    private Image[] _heartsPrefabs;
    private Color _brokenHeartColor;

    private void Awake()
    {
        _heartsPrefabs = _lifeContainer.GetComponentsInChildren<Image>();

        _brokenHeartColor = Color.white;
        _brokenHeartColor.a = 88;

        Reset();
    }

    public void InitializeUI()
    {
        for (int i = 0; i < _heartsPrefabs.Length; i++)
        {
            _heartsPrefabs[i].overrideSprite = _heartSprite;
        }
    }

    public void Reset()
    {
        _hearts = _maxHearts;
        InitializeUI();
    }

    public void UpdateUI(int hearts)
    {
        for (int i = 0; i < _maxHearts; i++)
        {
            if (i < hearts)
            {
                _heartsPrefabs[i].overrideSprite = _heartSprite;
                _heartsPrefabs[i].color = Color.white;
            }
            else
            {
                _heartsPrefabs[i].overrideSprite = _brokenHeartSprite;
                _heartsPrefabs[i].color = _brokenHeartColor;
            }
        }
    }

    public void RemoveHeart()
    {
        _hearts = (_hearts - 1 > -1) ? _hearts - 1 : 0;
        UpdateUI(_hearts);
    }

    public void AddHeart()
    {
        _hearts = (_hearts + 1 <= _maxHearts) ? _hearts + 1 : _maxHearts;
        UpdateUI(_hearts);
    }


}