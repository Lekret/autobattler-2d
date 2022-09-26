using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TextureImporterPostprocessor : AssetPostprocessor
    {
        private void OnPostprocessTexture(Texture2D texture)
        {
            var importer = (TextureImporter) assetImporter;
            importer.spritePixelsPerUnit = 32;
            importer.filterMode = FilterMode.Point;
        }
    }
}