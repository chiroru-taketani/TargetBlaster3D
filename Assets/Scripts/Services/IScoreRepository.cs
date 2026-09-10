using UnityEngine;

public interface IScoreRepository 
{
    int GetHighScore();
    void SaveHighScore(int score);
}
