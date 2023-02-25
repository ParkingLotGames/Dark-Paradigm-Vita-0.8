using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

namespace DP.PSVita.System
{
    /// <summary>
    /// A simple Unity plugin that demonstrates loading and unloading native modules through the use of DLLImport.
    /// </summary>
    public class SimpleNativePlugin : MonoBehaviour
    {
        /// <summary>
        /// Loads and starts a native module from a given file path.
        /// </summary>
        /// <param name="Path">The file path to the native module to load.</param>
        /// <returns>The loaded plugin's UID (Process ID) on success, otherwise a negative error code.</returns>
        [DllImport("NativePluginExample")]
        public static extern int loadStartModuleFromPath(string Path);

        /// <summary>
        /// Stops and unloads a previously loaded native module based on its UID.
        /// </summary>
        /// <param name="UID">The UID of the native module to stop and unload.</param>
        /// <returns>0 on success, or a negative error code.</returns>
        [DllImport("NativePluginExample")]
        public static extern int StopUnloadModuleFromUID(int UID);

        private string infoText;
        private int infoCount = 0;

        /// <summary>
        /// A coroutine that loads and starts a native module to overclock the device.
        /// </summary>
        /// <returns>An IEnumerator used for coroutine functionality.</returns>
        IEnumerator OverClock()
        {
            int id = loadStartModuleFromPath("app0:/Media/Plugins/oclockvita.suprx");
            if (id < 0)
            {
                yield return null;
            }
        }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            StartCoroutine(OverClock());
        }

        /// <summary>
        /// A coroutine that loads and unloads a native module after a set amount of time.
        /// </summary>
        /// <returns>An IEnumerator used for coroutine functionality.</returns>
        IEnumerator LoadAndUnloadModule()
        {
            yield return new WaitForSeconds(10);

            // app0: is the App folder in ux0:app or ur0:app; in this example, it will be ux0:app/ABCD12345
            int Result = loadStartModuleFromPath("app0:Media/Plugins/UnityOC.suprx");

            yield return new WaitForSeconds(10);

            int StopResult = StopUnloadModuleFromUID(Result);
        }
    }
}