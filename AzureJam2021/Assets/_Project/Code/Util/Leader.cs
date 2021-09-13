using UnityEngine;
using TMPro;

public class Leader : MonoBehaviour
{
    [SerializeField] private TMP_Text _leaderRank;
    [SerializeField] private TMP_Text _leaderName;
    [SerializeField] private TMP_Text _leaderScore;

    public void Initialize(string name, int score, int rank)
    {
        _leaderRank.SetText($"{ rank + 1}");
        _leaderName.SetText(name);
        _leaderScore.SetText(score.ToString());        
    }
}
