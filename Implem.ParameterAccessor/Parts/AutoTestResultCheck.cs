﻿namespace Implem.ParameterAccessor.Parts
{
    public class ResultCheck
    {
        public string ElementId;
        public string ElementXpath;
        public string ElementCss;
        public string ElementLinkText;
        public string ItemId;
        public string SearchString;
        public string ExpectedValue;
        public string ExecutionValue;
        public string Description;
        public int WaitTime;
        public CheckTypes CheckType;
    }

    public enum CheckTypes
    {
        Default,
        Existance,
        HasClass,
        HasNotClass,
        ReadOnly,
        DataRequiredTrue,
        DataRequiredFalse,
        Regex,
        SelectOptions,
        Label,
        CanReadRecordOnGrid,
        CanNotReadRecordOnGrid,
        CanUpdateRecordOnEditor,
        CanReadRecordOnEditor,
        CanNotReadRecordOnEditor,
        CanCrossSearchRecord,
        CanNotCrossSearchRecord
    }
}
