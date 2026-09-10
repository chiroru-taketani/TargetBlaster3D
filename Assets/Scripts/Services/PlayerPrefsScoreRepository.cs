using UnityEngine;

public class PlayerPrefsScoreRepository : IScoreRepository
{
    private const string HighScoreKey = "HIGH_SCORE";

    public int GetHighScore() => PlayerPrefs.GetInt(HighScoreKey,0);

    public void SaveHighScore(int score)
    {
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.Save();
        }
    }
}
