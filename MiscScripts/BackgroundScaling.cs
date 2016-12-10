using UnityEngine;


public class BackgroundScaling : MonoBehaviour {

    public float PictureWidth;
    public float AmountofPictures;
    public float PictureAdjustmentWidth;
    public float PictureAdjustmentHeight;



    private Transform Background;
	
	void Awake () {
        Background = transform;
        //new Vector3(PictureWidth / Screen.width, PicturHeight / Screen.height, 1);
        float scalewidth = Screen.width / AmountofPictures;
        Vector3 Scale = new Vector3(scalewidth / PictureWidth + PictureAdjustmentWidth, scalewidth / PictureWidth + PictureAdjustmentHeight, 1);
        Background.localScale = Scale;
    }
}
