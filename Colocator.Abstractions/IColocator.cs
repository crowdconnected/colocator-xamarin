using System;
using System.Collections;

namespace Colocator
{
    public interface IColocator
    {

		ColocatorDelegate Delegate { get; set; }

		// METHODS

		void StartWithAppKey(string appKey);

		void Stop();


		// PROPERTIES

		string DeviceId { get; }

		string TestLibraryIntegration { get; }


		// COMMON METHODS

		void AddAliasWithKey(string key, string value);

		void RequestLocation();

		void RegisterLocationListener();

		void UnregisterLocationListener();



		// iOS METHODS

		void AskMotionPermissions();

		void TriggerMotionPermissionPopUp();

		void TriggerBluetoothPermissionPopUp();

		void ReceivedSilentNotificationWithUserInfo(IDictionary userInfo, string key, Action<bool> completion);

		void UpdateLibraryBasedOnClientStatusWithClientKey(string key, bool isSilentNotification, Action<bool> completion);


		// ANDROID METHODS

		void ActivateForegroundService(string title, int icon, string channel);
	}

    public interface ColocatorDelegate
    {
	    void DidReceiveLocation(LocationResponse location);
    }

    public struct LocationResponse
    {
	    public double Latitude { get; set; }

		public double Longitude { get; set; }

		public double HeadingOffSet { get; set; }

		public double Error { get; set; }

		public ulong Timestamp { get; set; }
    }
}
