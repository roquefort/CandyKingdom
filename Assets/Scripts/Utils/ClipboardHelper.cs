#if UNITY_WEBGL && !UNITY_EDITOR
#define USE_WEBGL_VERSION
#endif 

using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RTLOL
{
    public static class ClipboardHelper
    {
#if USE_WEBGL_VERSION
        [DllImport("__Internal")] private static extern void RtlolClipboardHelper_SetClipboardText(string message, string text);
        [DllImport("__Internal")] private static extern string RtlolClipboardHelper_GetClipboardText(string message);
#else
        private static void RtlolClipboardHelper_SetClipboardText(string message, string text) { GUIUtility.systemCopyBuffer = text; }
        private static string RtlolClipboardHelper_GetClipboardText(string message) { return GUIUtility.systemCopyBuffer; }
#endif
        public static void SetClipboardText(string message, string text)
        {
            RtlolClipboardHelper_SetClipboardText(message, text);
        }

        public static string GetClipboardText(string message)
        {
            return RtlolClipboardHelper_GetClipboardText(message);
        }
    }
}
