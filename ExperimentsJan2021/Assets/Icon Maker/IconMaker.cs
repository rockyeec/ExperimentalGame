#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace Rock.Utilities
{

    public class IconMaker : MonoBehaviour
    {
        [SerializeField] RenderTexture renderTexture = null;
        [SerializeField] Camera bakeCam = null;
        [SerializeField] string spriteName = string.Empty;

        string SaveLocation
        {
            get
            {
                string saveLocation = Application.streamingAssetsPath + "/Icons/";

                if (!Directory.Exists(saveLocation))
                {
                    Directory.CreateDirectory(saveLocation);
                }

                return saveLocation;
            }
        }


        public void CreateIcon()
        {
            if (string.IsNullOrEmpty(spriteName))
            {
                spriteName = "icon";
            }

            string path = SaveLocation + spriteName;

            bakeCam.targetTexture = renderTexture;

            RenderTexture currentRt = RenderTexture.active;
            bakeCam.targetTexture.Release();
            RenderTexture.active = bakeCam.targetTexture;
            bakeCam.Render();

            int width = bakeCam.targetTexture.width;
            int height = bakeCam.targetTexture.height;

            Texture2D imgPng = new Texture2D(width, height, TextureFormat.ARGB32, false);
            imgPng.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            imgPng.Apply();
            RenderTexture.active = currentRt;
            byte[] bytesPng = imgPng.EncodeToPNG();
            File.WriteAllBytes(path + ".png", bytesPng);

            Debug.Log(spriteName + " Created!");
        }
    }

}
#endif