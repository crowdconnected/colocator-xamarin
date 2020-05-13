using System;
using System.Collections;

namespace Colocator
{
    public static class ColocatorMain 
    {
        private static Lazy<IColocator> instance =
            new Lazy<IColocator>(() => new ColocatorImplementation());

        public static IColocator Instance => instance.Value;
    }

    internal class ColocatorImplementation : IColocator
    {
        public string DeviceId => "Colocator iOS - Device ID";

        public string TestLibraryIntegration => "Colocator iOS - Integration test result";

        public ColocatorDelegate Delegate { get; set; }

        public void ActivateForegroundService(string title, int icon, string channel)
        {
            Console.WriteLine("Method available only on Android");
        }

        public void AddAliasWithKey(string key, string value)
        {
            Console.WriteLine("Added Alias");
        }

        public void AskMotionPermissions()
        {
            Console.WriteLine("Asked for permission");
        }

        public void ReceivedSilentNotificationWithUserInfo(IDictionary userInfo, string key, Action<bool> completion)
        {
            Console.WriteLine("Received SPN");
        }

        public void RegisterLocationListener()
        {
            Console.WriteLine("Registered for location updates");
        }

        public void RequestLocation()
        {
            Console.WriteLine("Request one location");
            LocationResponse loc = new LocationResponse();
            loc.Latitude = 1;
            loc.Longitude = 2;
            loc.HeadingOffSet = 3;
            loc.Error = 4;
            loc.Timestamp = 5;
            Delegate.DidReceiveLocation(loc);
        }

        public void StartWithAppKey(string appKey)
        {
			Console.WriteLine("Starting Colocator iOS ...");
		}

        public void Stop()
        {
			Console.WriteLine("Stop Colocator iOS");
		}

        public void TriggerBluetoothPermissionPopUp()
        {
            Console.WriteLine("Bluetooth Permission");
        }

        public void TriggerMotionPermissionPopUp()
        {
            Console.WriteLine("Motion Permission");
        }

        public void UnregisterLocationListener()
        {
            Console.WriteLine("Unregistered from location updates");
        }

        public void UpdateLibraryBasedOnClientStatusWithClientKey(string key, bool isSilentNotification, Action<bool> completion)
        {
            Console.WriteLine("Updated library based on status ");
        }
    }
}
