using System;
using System.Collections;
using System.Collections.Generic;
using Android.App;
using Net.Crowdconnected.Androidcolocator;
using Net.Crowdconnected.Androidcolocator.Connector;
using Plugin.CurrentActivity;

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
        public void StartWithAppKey(string appKey)
        {
            ICurrentActivity current = CrossCurrentActivity.Current;
            Application appl = current.Activity.Application;

            CoLocator.Start(appl, appKey);
        }

        public void Stop()
        {
            CoLocator.Instance().Stop();
        }

        public string DeviceId => CoLocator.Instance().DeviceId;

        public ColocatorDelegate Delegate { get; set; }

        public void ActivateForegroundService(string title, int icon, string channel)
        {
            ICurrentActivity current = CrossCurrentActivity.Current;
            Application appl = current.Activity.Application;

            CoLocator.SetServiceNotificationInfo(appl, title, icon, channel);
        }

        public void AddAliasWithKey(string key, string value)
        {
            CoLocator.Instance().AddAlias(key, value);
        }

        public void RegisterLocationListener()
        {
            CoLocator.Instance().RegisterLocationListener(new AndroidDelegate());
        }

        public void UnregisterLocationListener()
        {
            CoLocator.Instance().UnregisterLocationListener();
        }

        public void RequestLocation()
        {
            CoLocator.Instance().RequestLocation(new AndroidDelegate());
        }

        public string TestLibraryIntegration => "Method available only on iOS";

        public void TriggerBluetoothPermissionPopUp()
        {
            Console.WriteLine("Method available only on iOS");
        }

        public void TriggerMotionPermissionPopUp()
        {
            Console.WriteLine("Method available only on iOS");
        }

        public void ReceivedSilentNotificationWithUserInfo(IDictionary userInfo, string key, Action<bool> completion)
        {
            Console.WriteLine("Method available only on iOS");
        }

        public void UpdateLibraryBasedOnClientStatusWithClientKey(string key, bool isSilentNotification, Action<bool> completion)
        {
            Console.WriteLine("Method available only on iOS");
        }
    }

    internal class AndroidDelegate : Java.Lang.Object, ILocationCallback
    {
        public void OnLocationReceived(LocationResponse p0)
        {
            ColocatorLocationResponse loc = new ColocatorLocationResponse();
            loc.Latitude = p0.Latitude;
            loc.Longitude = p0.Longitude;
            loc.HeadingOffSet = p0.HeadingOffset;
            loc.Error = p0.Error;
            loc.Timestamp = (ulong)p0.Timestamp;

            ColocatorMain.Instance.Delegate.DidReceiveLocation(loc);
        }

        public void OnLocationsReceived(IList<LocationResponse> p0)
        {
            for (int i = 0; i < p0.Count; i++)
            {
                ColocatorLocationResponse loc = new ColocatorLocationResponse();
                loc.Latitude = p0[i].Latitude;
                loc.Longitude = p0[i].Longitude;
                loc.HeadingOffSet = p0[i].HeadingOffset;
                loc.Error = p0[i].Error;
                loc.Timestamp = (ulong)p0[i].Timestamp;

                ColocatorMain.Instance.Delegate.DidReceiveLocation(loc);
            }
        }
    }
}