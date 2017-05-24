using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileServer.ViewModels;

namespace MobileServer.Core
{
    public class DataSafe
    {
        public static GraphByName GraphByName { set; get; }
        public static GraphById GraphById { set; get; }
        public static GraphSettingsById GraphSettingsById { set; get; }
        public static GraphSettingsByName GraphSettingsByName { set; get; }
    }
}