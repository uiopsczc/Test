#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   https://blog.csdn.net/candycat1992/article/details/52067975   然后搜索HandleUtility.AddDefaultControl
    ///   控制默认鼠标或键盘事件
    /// </summary>
    public class SetDefaultControlScope : IDisposable
    {
        private readonly int _controlId;

        public SetDefaultControlScope(FocusType focusType)
        {
            _controlId = GUIUtility.GetControlID(focusType);
        }

        public void Dispose()
        {
            HandleUtility.AddDefaultControl(_controlId);
        }
    }
}
#endif