using UnityEngine;
using System.Collections;
using System.IO;

public class ShareIntent : MonoBehaviour {

    private string shareText = "Beat my score of {0}!\n";
    private string gameLink = "Download the game on the Play Store at: " + "\nhttps://play.google.com/store/apps/details?id=com.TITS.StackDestroy&pcampaignid=GPC_shareGame";
    private string subject = "Stack and Destroy";
    private bool isProcessing = false;

    [SerializeField]
    private Camera renderCamera;

    
    public void Share()
    {
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshot());
        }
    }

    private byte[] TakeScreenShot()
    {
        RenderTexture screenshotTexture = new RenderTexture(Screen.width, Screen.height, 24);

        renderCamera.targetTexture = screenshotTexture;
        renderCamera.Render();

        Texture2D imageOverview = new Texture2D(screenshotTexture.width, screenshotTexture.height, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), 0, 0);
        imageOverview.Apply();

        // Reset Render Texture
        renderCamera.targetTexture = null;
        renderCamera.enabled = false;

        Destroy(screenshotTexture);

        return imageOverview.EncodeToPNG();
    }

    private IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        // Activate Rendercamera
        renderCamera.enabled = true;
        yield return new WaitForEndOfFrame();

        byte[] dataToSave = TakeScreenShot();

        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
#if UNITY_EDITOR
        UnityEditor.EditorUtility.RevealInFinder(destination);
#endif
        Debug.Log("[ShareIntent] Screenshot destination: " + destination);
        File.WriteAllBytes(destination, dataToSave);

#if UNITY_ANDROID
        if (!Application.isEditor)
        {
            string newShareText = string.Format(shareText, HighScore.GetScore.ToString("0"));

            // Android intent code taken from http://www.thegamecontriver.com/2015/09/unity-share-post-image-to-facebook.html?showComment=1455523299779

            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), newShareText + gameLink);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // Use chooser instead of allowing intent to be defaulted - https://forum-old.unity3d.com/threads/creating-a-share-button-intent-for-android-in-unity-that-forces-the-chooser.335751/#post-2174828
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
            currentActivity.Call("startActivity", chooser);
        }
#endif
        isProcessing = false;
    }


}
