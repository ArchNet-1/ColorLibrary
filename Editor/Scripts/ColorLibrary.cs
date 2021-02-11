using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Description : Color Library is a link between color and enum or integer
/// Avoid mass refacto on a change  on a color
/// @author : Killian Chicoisne
/// </summary>
namespace ArchNet.Library.Color
{
    [System.Serializable]
    public class ColorData
    {
        public ColorData(int colorKey, UnityEngine.Color colorValue)
        {
            _colorKey = colorKey;
            _colorValue = colorValue;
        }
        public int _colorKey;
        public UnityEngine.Color _colorValue;


    }


    [CreateAssetMenu(fileName = "NewColorLibrary", menuName = "ArchNet/ColorLibrary")]
    public class ColorLibrary : ScriptableObject
    {
        public enum KeyType
        {
            NONE,
            ENUM,
            INT
        }

        #region SerializeField

        // The Full namespace enum path
        [SerializeField]
        private string _enumPath;

        // Our reorderable buffer list to fill the Dictionnary
        [SerializeField]
        private List<ColorData> _colorList;

        [SerializeField]
        bool _expandedSettings = true;

        [SerializeField]
        bool _forceDefaultValue = false;

        [SerializeField]
        private int _defaultValue = 0;

        [SerializeField]
        KeyType _keyType = KeyType.NONE;

        #endregion

        #region Private Properties

        // Our final Dictionnary
        private Dictionary<int, UnityEngine.Color> _colorDict;
        private string[] _enumValues;

        #endregion

        #region Public Methods

        public UnityEngine.Color GetColor(int keyValue)
        {
            if (CheckExistingColor(keyValue))
            {
                return _colorDict[keyValue];
            }
            // return pink color by default
            return new UnityEngine.Color(1, 0, 1);
        }

        public int GetMaxValue()
        {
            int lResult = 0;

            foreach (ColorData lColorData in _colorList)
            {
                if (lResult < lColorData._colorKey)
                {
                    lResult = lColorData._colorKey;
                }
            }
            
            return lResult;
        }


        public string[] GetEnumValues(string enumName)
        {
            Type type = GetEnumType(enumName);
            if (type != null)
            {
                this._enumPath = enumName;
                _enumValues = Enum.GetNames(type);
                return _enumValues;
            }

            return null;
        }

        public string GetEnumPath()
        {
            return _enumPath;
        }

        public Type GetEnumType(string enumName)
        {
            if (false == string.IsNullOrEmpty(enumName))
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var type = assembly.GetType(enumName);
                    if (type == null)
                        continue;
                    if (type.IsEnum)
                        return type;
                }
            }
            return null;
        }

        #endregion

        #region Private Methods

        private bool CheckExistingColor(int enumValue)
        {
            if (_colorDict == null)
            {
                if (_colorList != null)
                {
                    this.SaveDictionnary();
                }

                if (_colorList.Count == 0)
                {
                    Debug.LogWarning("Color Library \'" + this.name + "\' doesn't contain anything");
                    return false;
                }
            }
            if (false == IsInList(enumValue))
            {
                string[] lEnumValues = this.GetEnumValues(_enumPath);
                Debug.LogWarning("Library do not contain a color for key: " + enumValue);
                return false;
            }

            if (_colorDict.ContainsKey(enumValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsInList(int pValue)
        {
            bool lResult = false;

            foreach (int lValue in _colorDict.Keys)
            {
                if (pValue == lValue)
                {
                    lResult = true;
                }
            }

            return lResult;
        }

        #endregion

        #region Editor Methods

        /// <summary>
        /// FOR CUSTOM EDITOR PURPOSE ONLY! DO NOT USE
        /// </summary>
        public int GetKeyType()
        {
            return (int)this._keyType;
        }

        /// <summary>
        /// FOR CUSTOM EDITOR PURPOSE ONLY! DO NOT USE
        /// </summary>
        public bool isKeyTypeUpToDate(int pKeyValue)
        {
            if ((int)this._keyType == pKeyValue)
            { return true; }
            else
            { return false; }
        }

        /// <summary>
        /// FOR CUSTOM EDITOR PURPOSE ONLY! DO NOT USE
        /// </summary>
        public void SaveDictionnary()
        {
            if (_colorList == null)
            { return; }

            if (_colorList.Count == 0)
            {
                Debug.LogWarning("The color List is empty");
            }
            _colorDict = new Dictionary<int, UnityEngine.Color>();
            for (int i = 0; i < _colorList.Count; i++)
            {
                if (_colorList[i]._colorKey != this._defaultValue)
                {
                    // Check that there's not two color for the same enum
                    if (_colorDict.ContainsKey(_colorList[i]._colorKey))
                    {
                        if (_colorDict[_colorList[i]._colorKey] != _colorList[i]._colorValue)
                        {
                            if (this._keyType == KeyType.ENUM)
                            {
                                Debug.LogWarning("Color \'" + _colorDict[_colorList[i]._colorKey] + "\' is already defined for \'" + this._enumValues[_colorList[i]._colorKey] + "\'\nCannot define \'" + _colorList[i]._colorValue + "\' as well.");
                            }
                            else
                            {
                                Debug.LogWarning("Color \'" + _colorDict[_colorList[i]._colorKey] + "\' is already defined for \'" + _colorList[i]._colorKey + "\'\nCannot define \'" + _colorList[i]._colorValue + "\' as well.");
                            }
                        }
                    }
                    else
                    {
                        _colorDict.Add(_colorList[i]._colorKey, _colorList[i]._colorValue);
                    }
                }
            }
        }

        public void AddAColor()
        {
            this._colorList.Add(new ColorData(this._defaultValue, UnityEngine.Color.black));
        }

        #endregion
    }
}

