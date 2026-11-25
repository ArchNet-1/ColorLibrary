// ArchNet · Color Library
// Your game's palette in one asset: each color is stored under a key (an enum value such
// as Theme.Primary, or an int), so changing a color updates every place that uses it.
// Create one with  Assets > Create > ArchNet > Color Library.
// Docs: https://archnet-plateform.fr/#ColorLibrary

using System;
using UnityEngine;

namespace ArchNet.Library
{
    [CreateAssetMenu(fileName = "NewColorLibrary", menuName = "ArchNet/Color Library")]
    public class ColorLibrary : KeyedLibrary<Color>
    {
        public Color GetColor(int key) => Get(key);
        public Color GetColor(Enum key) => Get(key);
    }
}
