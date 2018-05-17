using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.NetworkInformation;
using MyPatchSG.Utils;
using MyPatchSG.Droid.Utils;

namespace MyPatchSG.Droid.Utils
{
    public class DeviceUtil
    {
        public DeviceUtil()
        {

        }

        public static string GetMACAddress()
        {
            try
            {
                List<NetworkInterface> all = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());

                foreach (var nif in all)
                {
                    if (!nif.Name.Equals("wlan0", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    var address = (nif as NetworkInterface).GetPhysicalAddress();
                    var macBytes = address.GetAddressBytes();

                    if (macBytes == null) {
                        continue;
                    }

                    var sb = new StringBuilder();
                    foreach (var b in macBytes)
                    {
                        sb.Append((b & 0xFF).ToString("X2") + ":");
                    }

                    return sb.ToString().Remove(sb.Length - 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return "02:00:00:00:00:00";
        }
    }
}