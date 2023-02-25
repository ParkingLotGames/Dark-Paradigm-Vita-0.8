using UnityEngine;
using UnityEngine.UI;

namespace DP
{
    /// <summary>
    /// A script to detect and display information about the network interfaces on a PS Vita device.
    /// </summary>
    public class DetectPSVitaHardware : MonoBehaviour
    {
        /// <summary>
        /// This function is called before the first frame update.
        /// </summary>
        private void Start()
        {
            // Get a reference to the Text component that is a child of this GameObject.
            Text text = GetComponentInChildren<Text>();

            // Get an array of network interfaces on the device.
            System.Net.NetworkInformation.NetworkInterface[] nets = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            // Loop through the network interfaces and add their descriptions, names, IDs, speeds, types, and multicast support to the Text component.
            for (int i = 0; i < nets.Length; i++)
            {
                text.text += $"\n  Description: {nets[i].Description}, Name: {nets[i].Name}, ID: {nets[i].Id}, Speed: {nets[i].Speed}, Type: {nets[i].NetworkInterfaceType}, Multicast Support: {nets[i].SupportsMulticast}";
            }

            // If no network interfaces were found, add a message to the Text component.
            if (nets.Length == 0)
                text.text += "No interfaces found";
        }
    }
}