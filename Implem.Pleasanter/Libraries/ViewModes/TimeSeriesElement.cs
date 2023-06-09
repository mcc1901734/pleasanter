﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Server;
using System;
namespace Implem.Pleasanter.Libraries.ViewModes
{
    public class TimeSeriesElement
    {
        public long Id;
        public int Ver;
        public DateTime UpdatedTime;
        public DateTime HorizontalAxis;
        public string Index;
        public decimal Value;
        public bool IsHistory;
        public bool Latest;

        public TimeSeriesElement(
            Context context,
            bool userColumn,
            long id,
            int ver,
            DateTime horizontalAxis,
            string index,
            decimal value,
            bool isHistory)
        {
            Id = id;
            Ver = ver;
            HorizontalAxis = horizontalAxis;
            Index = userColumn && SiteInfo.User(
                context: context,
                userId: index.ToInt()).Anonymous()
                    ? "\t"
                    : index == string.Empty
                        ? "\t"
                        : index;
            Value = value;
            IsHistory = isHistory;
        }
    }
}