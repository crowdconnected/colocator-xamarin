using System;
using System.Collections;
using Foundation;

namespace Colocator
{
    public static class ColocatorMain 
    {
        private static Lazy<IColocator> instance =
            new Lazy<IColocator>(() => new ColocatorImplementation());

        public static IColocator Instance => instance.Value;
    }

    internal class ColocatorImplementation : CCLocation.CCLocationDelegate, IColocator
    {
        public string DeviceId => CCLocation.CCLocation.SharedInstance.DeviceId;

        public string TestLibraryIntegration => CCLocation.CCLocation.SharedInstance.TestLibraryIntegration;

        public ColocatorDelegate Delegate { get; set; }

        public void StartWithAppKey(string appKey)
        {
            CCLocation.CCLocation.SharedInstance.StartWithApiKey(appKey, null);
            CCLocation.CCLocation.SharedInstance.Delegate = this;
        }

        public void Stop()
        {
            CCLocation.CCLocation.SharedInstance.Stop();
        }

        public void AddAliasWithKey(string key, string value)
        {
            CCLocation.CCLocation.SharedInstance.AddAliasWithKey(key, value);
        }

        public void TriggerBluetoothPermissionPopUp()
        {
            CCLocation.CCLocation.SharedInstance.TriggerBluetoothPermissionPopUp();
        }

        public void TriggerMotionPermissionPopUp()
        {
            CCLocation.CCLocation.SharedInstance.TriggerMotionPermissionPopUp();
        }

        public void ReceivedSilentNotificationWithUserInfo(IDictionary userInfo, string key, Action<bool> completion)
        {
            NSDictionary convertedDict = userInfo as NSDictionary;
            CCLocation.CCLocation.SharedInstance.ReceivedSilentNotificationWithUserInfo(convertedDict, key, completion: (response) => { completion(response);  });
        }

        public void RegisterLocationListener()
        {
            CCLocation.CCLocation.SharedInstance.RegisterLocationListener();
        }

        public void RequestLocation()
        {
            CCLocation.CCLocation.SharedInstance.RequestLocation();
        }

        public void UnregisterLocationListener()
        {
            CCLocation.CCLocation.SharedInstance.UnregisterLocationListener();
        }

        public void UpdateLibraryBasedOnClientStatusWithClientKey(string key, bool isSilentNotification, Action<bool> completion)
        {
            CCLocation.CCLocation.SharedInstance.UpdateLibraryBasedOnClientStatusWithClientKey(key, false, completion: (response) => { completion(response); });
        }

        public void ActivateForegroundService(string title, int icon, string channel)
        {
            Console.WriteLine("Method available only on Android");
        }

        public override void CcLocationDidConnect()
        {
            Console.WriteLine("Colocator connected successfully");
        }

        public override void CcLocationDidFailWithErrorWithError(NSError error)
        {
            Console.WriteLine("Colocator failed to connect with error " + error);
        }

        public override void DidReceiveCCLocation(CCLocation.LocationResponse location)
        {
            ColocatorLocationResponse loc = new ColocatorLocationResponse();
            loc.Latitude = location.Latitude;
            loc.Longitude = location.Longitude;
            loc.HeadingOffSet = location.HeadingOffSet;
            loc.Error = location.Error;
            loc.Timestamp = location.Timestamp;
            loc.Floor = location.Floor;

            Delegate.DidReceiveLocation(loc);
        }
    }
}
