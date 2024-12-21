using UnityEngine;
public interface IInteractable
{
    Texture2D CursorTexture { get; }
    void OnClick();
}
