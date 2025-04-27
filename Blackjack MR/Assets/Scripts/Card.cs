using UnityEngine;

public class Card : MonoBehaviour
{
    public string rank;
    public string suit;
    public GameObject frontFace;
    public GameObject backFace;

    public void SetFaceDown(bool isFaceDown)
    {
        if (frontFace != null && backFace != null)
        {
            frontFace.SetActive(!isFaceDown);
            backFace.SetActive(isFaceDown);
        }
    }
}
