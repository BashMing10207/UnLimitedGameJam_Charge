using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public FadeIn FadeIn;
    public void Quit()
    {
        Application.Quit();
    }

    public void TransitionLobby()
    {
        FadeIn.FadeStart("LobbyScene");   
    }
}
