﻿//using MediaAppSample.Core;
//using MediaAppSample.Core.Models;
//using MediaAppSample.Core.Services;
//using FlurryWin8SDK;
//using FlurryWin8SDK.Models;
//using System;
//using System.Collections.Generic;

//namespace MediaAppSample.UI.Services
//{
//    /// <summary>
//    /// Analytics wrapper for Flurry analytics service.
//    /// </summary>
//    public sealed class FlurryAnalyticsService : AnalyticsServiceBase
//    {
//        public FlurryAnalyticsService(string key)
//        {
//            if (string.IsNullOrWhiteSpace(key))
//                throw new ArgumentNullException(nameof(key));

//            Api.StartSession(key);
//        }

//        protected override void Initialize()
//        {
//            Api.SetVersion(Platform.Current.AppInfo.VersionNumber.ToString());
//            base.Initialize();
//        }

//        public override void NewPageView(Type pageType)
//        {
//            Api.LogPageView();
//        }

//        public override void Event(string eventName, Dictionary<string, string> metrics = null)
//        {
//            List<Parameter> list = null;
//            if (metrics != null)
//            {
//                list = new List<Parameter>();
//                foreach (var metric in metrics)
//                    list.Add(new Parameter(metric.Key, metric.Value.ToString()));
//            }

//            if (list != null && list.Count > 0)
//                Api.LogEvent(eventName, list);
//            else
//                Api.LogEvent(eventName);
//        }

//        public override void Error(Exception ex, string message = null)
//        {
//            Api.LogError(message, ex);
//        }

//        public override void SetCurrentLocation(ILocationModel loc)
//        {
//            if (loc != null)
//                Api.SetLocation(loc.Latitude, loc.Longitude, 0);
//        }

//        public override void SetUsername(string username)
//        {
//            Api.SetUserId(username);
//        }

//        public override void Dispose()
//        {
//            Api.EndSession().AsTask().Wait();
//        }
//    }
//}