﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using System;
namespace Implem.Pleasanter.Libraries.Extensions
{
    public static class ToControlExtensions
    {
        public static string ToControl(
            this bool self, Context context, SiteSettings ss, Column column)
        {
            return self.ToString();
        }

        public static string ToControl(
            this DateTime self, Context context, SiteSettings ss, Column column)
        {
            return column.DisplayControl(
                context: context,
                value: self.ToLocal(context: context));
        }

        public static string ToControl(
            this string self, Context context, SiteSettings ss, Column column)
        {
            return self;
        }

        public static string ToControl(
            this int self, Context context, SiteSettings ss, Column column)
        {
            return self.ToString(column.StringFormat);
        }

        public static string ToControl(
            this long self, Context context, SiteSettings ss, Column column)
        {
            return self.ToString(column.StringFormat);
        }

        public static string ToControl(
            this double self, Context context, SiteSettings ss, Column column)
        {
            return self.ToString(column.StringFormat);
        }

        public static string ToControl(
            this Enum self, Context context, SiteSettings ss, Column column)
        {
            return self.ToInt().ToString();
        }
    }
}