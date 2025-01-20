using UnityEngine.SceneManagement;

public class BackToMenuButton : AbstractButton
{
    protected override void OnClick() => SceneManager.LoadScene(0);
}