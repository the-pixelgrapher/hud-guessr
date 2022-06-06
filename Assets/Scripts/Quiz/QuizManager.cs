using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public enum GameMode
    {
        MultiChoice,
        TextField
    }

    public GameMode gameMode;

    [SerializeField]
    private GameDatabase database;
    [SerializeField]
    private GameHistory history;

    [SerializeField]
    private HUDGraphic[] hudGraphics;
    [SerializeField]
    private MultiChoiceAnswer multiChoiceAnswer;

    private GameDatabase.GameMetadata chosenGame;

    void Start()
    {
        //InitRandomGame();
    }

    public void InitRandomGame()
    {
        chosenGame = database.GetRandomGame();
        foreach (HUDGraphic _graphic in hudGraphics)
        {
            _graphic.InitHUDImage(chosenGame);
        }

        switch (gameMode)
        {
            case GameMode.MultiChoice:
                multiChoiceAnswer.InitGameData(chosenGame);
                break;

            case GameMode.TextField:
                break;
        }
    }

    public void Guess()
    {
        switch (gameMode)
        {
            case GameMode.MultiChoice:
                if (multiChoiceAnswer.TestAnswer()) {Debug.Log("Correct!");}
                else{Debug.Log("Incorrect. Correct answer was: " + chosenGame.displayName);}
                break;

            case GameMode.TextField:
                break;
        }
    }
}
