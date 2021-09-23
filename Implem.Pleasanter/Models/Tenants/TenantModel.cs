﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Classes;
using Implem.Libraries.DataSources.SqlServer;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.Extensions;
using Implem.Pleasanter.Libraries.General;
using Implem.Pleasanter.Libraries.Html;
using Implem.Pleasanter.Libraries.HtmlParts;
using Implem.Pleasanter.Libraries.Models;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.ServerScripts;
using Implem.Pleasanter.Libraries.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using static Implem.Pleasanter.Libraries.ServerScripts.ServerScriptModel;
namespace Implem.Pleasanter.Models
{
    [Serializable]
    public class TenantModel : BaseModel
    {
        public int TenantId = 0;
        public string TenantName = string.Empty;
        public Title Title = new Title();
        public string Body = string.Empty;
        public ContractSettings ContractSettings = new ContractSettings();
        public DateTime ContractDeadline = 0.ToDateTime();
        public bool DisableAllUsersPermission = false;
        public bool DisableStartGuide = false;
        public LogoTypes LogoType = (LogoTypes)0;
        public string HtmlTitleTop = "[ProductName]";
        public string HtmlTitleSite = "[ProductName]";
        public string HtmlTitleRecord = "[ProductName]";
        public string TopStyle = string.Empty;
        public string TopScript = string.Empty;
        public int SavedTenantId = 0;
        public string SavedTenantName = string.Empty;
        public string SavedTitle = string.Empty;
        public string SavedBody = string.Empty;
        public string SavedContractSettings = string.Empty;
        public DateTime SavedContractDeadline = 0.ToDateTime();
        public bool SavedDisableAllUsersPermission = false;
        public bool SavedDisableStartGuide = false;
        public int SavedLogoType = 0;
        public string SavedHtmlTitleTop = "[ProductName]";
        public string SavedHtmlTitleSite = "[ProductName]";
        public string SavedHtmlTitleRecord = "[ProductName]";
        public string SavedTopStyle = string.Empty;
        public string SavedTopScript = string.Empty;

        public bool TenantId_Updated(Context context, Column column = null)
        {
            return TenantId != SavedTenantId &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != TenantId);
        }

        public bool TenantName_Updated(Context context, Column column = null)
        {
            return TenantName != SavedTenantName && TenantName != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != TenantName);
        }

        public bool Title_Updated(Context context, Column column = null)
        {
            return Title.Value != SavedTitle && Title.Value != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != Title.Value);
        }

        public bool Body_Updated(Context context, Column column = null)
        {
            return Body != SavedBody && Body != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != Body);
        }

        public bool ContractSettings_Updated(Context context, Column column = null)
        {
            return ContractSettings?.RecordingJson() != SavedContractSettings && ContractSettings?.RecordingJson() != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != ContractSettings?.RecordingJson());
        }

        public bool DisableAllUsersPermission_Updated(Context context, Column column = null)
        {
            return DisableAllUsersPermission != SavedDisableAllUsersPermission &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToBool() != DisableAllUsersPermission);
        }

        public bool DisableStartGuide_Updated(Context context, Column column = null)
        {
            return DisableStartGuide != SavedDisableStartGuide &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToBool() != DisableStartGuide);
        }

        public bool LogoType_Updated(Context context, Column column = null)
        {
            return LogoType.ToInt() != SavedLogoType &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToInt() != LogoType.ToInt());
        }

        public bool HtmlTitleTop_Updated(Context context, Column column = null)
        {
            return HtmlTitleTop != SavedHtmlTitleTop && HtmlTitleTop != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != HtmlTitleTop);
        }

        public bool HtmlTitleSite_Updated(Context context, Column column = null)
        {
            return HtmlTitleSite != SavedHtmlTitleSite && HtmlTitleSite != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != HtmlTitleSite);
        }

        public bool HtmlTitleRecord_Updated(Context context, Column column = null)
        {
            return HtmlTitleRecord != SavedHtmlTitleRecord && HtmlTitleRecord != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != HtmlTitleRecord);
        }

        public bool TopStyle_Updated(Context context, Column column = null)
        {
            return TopStyle != SavedTopStyle && TopStyle != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != TopStyle);
        }

        public bool TopScript_Updated(Context context, Column column = null)
        {
            return TopScript != SavedTopScript && TopScript != null &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.GetDefaultInput(context: context).ToString() != TopScript);
        }

        public bool ContractDeadline_Updated(Context context, Column column = null)
        {
            return ContractDeadline != SavedContractDeadline &&
                (column == null ||
                column.DefaultInput.IsNullOrEmpty() ||
                column.DefaultTime(context: context).Date != ContractDeadline.Date);
        }

        public List<int> SwitchTargets;

        public TenantModel()
        {
        }

        public TenantModel(
            Context context,
            SiteSettings ss,
            Dictionary<string, string> formData = null,
            bool setByApi = false,
            MethodTypes methodType = MethodTypes.NotSet)
        {
            OnConstructing(context: context);
            Context = context;
            TenantId = context.TenantId;
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            if (setByApi) SetByApi(context: context, ss: ss);
            MethodType = methodType;
            OnConstructed(context: context);
        }

        public TenantModel(
            Context context,
            SiteSettings ss,
            int tenantId,
            Dictionary<string, string> formData = null,
            bool setByApi = false,
            bool clearSessions = false,
            List<int> switchTargets = null,
            MethodTypes methodType = MethodTypes.NotSet)
        {
            OnConstructing(context: context);
            Context = context;
            TenantId = context.TenantId;
            if (context.QueryStrings.ContainsKey("ver"))
            {
                Get(context: context,
                    tableType: Sqls.TableTypes.NormalAndHistory,
                    where: Rds.TenantsWhereDefault(
                        context: context,
                        tenantModel: this)
                            .Tenants_Ver(context.QueryStrings.Int("ver")), ss: ss);
            }
            else
            {
                Get(context: context, ss: ss);
            }
            if (clearSessions) ClearSessions(context: context);
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            if (setByApi) SetByApi(context: context, ss: ss);
            SwitchTargets = switchTargets;
            MethodType = methodType;
            OnConstructed(context: context);
        }

        public TenantModel(
            Context context,
            SiteSettings ss,
            DataRow dataRow,
            Dictionary<string, string> formData = null,
            string tableAlias = null)
        {
            OnConstructing(context: context);
            Context = context;
            TenantId = context.TenantId;
            if (dataRow != null)
            {
                Set(
                    context: context,
                    ss: ss,
                    dataRow: dataRow,
                    tableAlias: tableAlias);
            }
            if (formData != null)
            {
                SetByForm(
                    context: context,
                    ss: ss,
                    formData: formData);
            }
            OnConstructed(context: context);
        }

        private void OnConstructing(Context context)
        {
        }

        private void OnConstructed(Context context)
        {
        }

        public void ClearSessions(Context context)
        {
        }

        public TenantModel Get(
            Context context,
            SiteSettings ss,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlColumnCollection column = null,
            SqlJoinCollection join = null,
            SqlWhereCollection where = null,
            SqlOrderByCollection orderBy = null,
            SqlParamCollection param = null,
            bool distinct = false,
            int top = 0)
        {
            where = where ?? Rds.TenantsWhereDefault(
                context: context,
                tenantModel: this);
            column = (column ?? Rds.TenantsDefaultColumns());
            join = join ??  Rds.TenantsJoinDefault();
            Set(context, ss, Repository.ExecuteTable(
                context: context,
                statements: Rds.SelectTenants(
                    tableType: tableType,
                    column: column,
                    join: join,
                    where: where,
                    orderBy: orderBy,
                    param: param,
                    distinct: distinct,
                    top: top)));
            return this;
        }

        public TenantApiModel GetByApi(Context context, SiteSettings ss)
        {
            var data = new TenantApiModel()
            {
                ApiVersion = context.ApiVersion
            };
            ss.ReadableColumns(context: context, noJoined: true).ForEach(column =>
            {
                switch (column.ColumnName)
                {
                    case "TenantId": data.TenantId = TenantId; break;
                    case "Ver": data.Ver = Ver; break;
                    case "TenantName": data.TenantName = TenantName; break;
                    case "Title": data.Title = Title.Value; break;
                    case "Body": data.Body = Body; break;
                    case "ContractSettings": data.ContractSettings = ContractSettings?.RecordingJson(); break;
                    case "ContractDeadline": data.ContractDeadline = ContractDeadline.ToLocal(context: context); break;
                    case "DisableAllUsersPermission": data.DisableAllUsersPermission = DisableAllUsersPermission; break;
                    case "DisableStartGuide": data.DisableStartGuide = DisableStartGuide; break;
                    case "LogoType": data.LogoType = LogoType.ToInt(); break;
                    case "HtmlTitleTop": data.HtmlTitleTop = HtmlTitleTop; break;
                    case "HtmlTitleSite": data.HtmlTitleSite = HtmlTitleSite; break;
                    case "HtmlTitleRecord": data.HtmlTitleRecord = HtmlTitleRecord; break;
                    case "TopStyle": data.TopStyle = TopStyle; break;
                    case "TopScript": data.TopScript = TopScript; break;
                    case "Creator": data.Creator = Creator.Id; break;
                    case "Updator": data.Updator = Updator.Id; break;
                    case "CreatedTime": data.CreatedTime = CreatedTime.Value.ToLocal(context: context); break;
                    case "UpdatedTime": data.UpdatedTime = UpdatedTime.Value.ToLocal(context: context); break;
                    case "Comments": data.Comments = Comments.ToLocal(context: context).ToJson(); break;
                    default: 
                        data.Value(
                            context: context,
                            column: column,
                            value: Value(
                                context: context,
                                column: column,
                                toLocal: true));
                        break;
                }
            });
            return data;
        }

        public ErrorData Create(
            Context context,
            SiteSettings ss,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlParamCollection param = null,
            bool otherInitValue = false,
            bool get = true)
        {
            TenantId = context.TenantId;
            var statements = new List<SqlStatement>();
            statements.AddRange(CreateStatements(
                context: context,
                ss: ss,
                tableType: tableType,
                param: param,
                otherInitValue: otherInitValue));
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                selectIdentity: true,
                statements: statements.ToArray());
            TenantId = (response.Id ?? TenantId).ToInt();
            if (get) Get(context: context, ss: ss);
            return new ErrorData(type: Error.Types.None);
        }

        public List<SqlStatement> CreateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            Sqls.TableTypes tableType = Sqls.TableTypes.Normal,
            SqlParamCollection param = null,
            bool otherInitValue = false)
        {
            var statements = new List<SqlStatement>();
            statements.AddRange(new List<SqlStatement>
            {
                Rds.InsertTenants(
                    dataTableName: dataTableName,
                    tableType: tableType,
                    selectIdentity: true,
                    param: param ?? Rds.TenantsParamDefault(
                        context: context,
                        ss: ss,
                        tenantModel: this,
                        setDefault: true,
                        otherInitValue: otherInitValue))
            });
            return statements;
        }

        public ErrorData Update(
            Context context,
            SiteSettings ss,
            SqlParamCollection param = null,
            List<SqlStatement> additionalStatements = null,
            bool otherInitValue = false,
            bool setBySession = true,
            bool get = true)
        {
            if (setBySession)
            {
                SetBySession(context: context);
            }
            var statements = new List<SqlStatement>();
            statements.AddRange(UpdateStatements(
                context: context,
                ss: ss,
                param: param,
                otherInitValue: otherInitValue,
                additionalStatements: additionalStatements));
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            if (response.Event == "Conflicted")
            {
                return new ErrorData(
                    type: Error.Types.UpdateConflicts,
                    id: TenantId);
            }
            if (get)
            {
                Get(context: context, ss: ss);
            }
            return new ErrorData(type: Error.Types.None);
        }

        public List<SqlStatement> UpdateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            SqlParamCollection param = null,
            bool otherInitValue = false,
            List<SqlStatement> additionalStatements = null)
        {
            var timestamp = Timestamp.ToDateTime();
            var statements = new List<SqlStatement>();
            var where = Rds.TenantsWhereDefault(
                context: context,
                tenantModel: this)
                    .UpdatedTime(timestamp, _using: timestamp.InRange());
            if (Versions.VerUp(
                context: context,
                ss: ss,
                verUp: VerUp))
            {
                statements.Add(Rds.TenantsCopyToStatement(
                    where: where,
                    tableType: Sqls.TableTypes.History,
                    ColumnNames()));
                Ver++;
            }
            statements.AddRange(UpdateStatements(
                context: context,
                ss: ss,
                dataTableName: dataTableName,
                where: where,
                param: param,
                otherInitValue: otherInitValue));
            if (additionalStatements?.Any() == true)
            {
                statements.AddRange(additionalStatements);
            }
            return statements;
        }

        private List<SqlStatement> UpdateStatements(
            Context context,
            SiteSettings ss,
            string dataTableName = null,
            SqlWhereCollection where = null,
            SqlParamCollection param = null,
            bool otherInitValue = false)
        {
            return new List<SqlStatement>
            {
                Rds.UpdateTenants(
                    dataTableName: dataTableName,
                    where: where,
                    param: param ?? Rds.TenantsParamDefault(
                        context: context,
                        ss: ss,
                        tenantModel: this,
                        otherInitValue: otherInitValue)),
                new SqlStatement(Def.Sql.IfConflicted.Params(TenantId)) {
                    IfConflicted = true,
                    Id = TenantId
                }
            };
        }

        public ErrorData UpdateOrCreate(
            Context context,
            SiteSettings ss,
            SqlWhereCollection where = null,
            SqlParamCollection param = null)
        {
            SetBySession(context: context);
            var statements = new List<SqlStatement>
            {
                Rds.UpdateOrInsertTenants(
                    where: where ?? Rds.TenantsWhereDefault(
                        context: context,
                        tenantModel: this),
                    param: param ?? Rds.TenantsParamDefault(
                        context: context,
                        ss: ss,
                        tenantModel: this,
                        setDefault: true))
            };
            var response = Repository.ExecuteScalar_response(
                context: context,
                transactional: true,
                selectIdentity: true,
                statements: statements.ToArray());
            TenantId = (response.Id ?? TenantId).ToInt();
            Get(context: context, ss: ss);
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData Delete(Context context, SiteSettings ss, bool notice = false)
        {
            var statements = new List<SqlStatement>();
            var where = Rds.TenantsWhere().TenantId(TenantId);
            statements.AddRange(new List<SqlStatement>
            {
                Rds.DeleteTenants(
                    factory: context,
                    where: where)
            });
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: statements.ToArray());
            var tenantHash = SiteInfo.TenantCaches.Get(context.TenantId)?.TenantHash;
            if (tenantHash.Keys.Contains(TenantId))
            {
                tenantHash.Remove(TenantId);
            }
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData Restore(Context context, SiteSettings ss,int tenantId)
        {
            TenantId = tenantId;
            Repository.ExecuteNonQuery(
                context: context,
                connectionString: Parameters.Rds.OwnerConnectionString,
                transactional: true,
                statements: new SqlStatement[]
                {
                    Rds.RestoreTenants(
                        factory: context,
                        where: Rds.TenantsWhere().TenantId(TenantId))
                });
            return new ErrorData(type: Error.Types.None);
        }

        public ErrorData PhysicalDelete(
            Context context, SiteSettings ss,Sqls.TableTypes tableType = Sqls.TableTypes.Normal)
        {
            Repository.ExecuteNonQuery(
                context: context,
                transactional: true,
                statements: Rds.PhysicalDeleteTenants(
                    tableType: tableType,
                    param: Rds.TenantsParam().TenantId(TenantId)));
            return new ErrorData(type: Error.Types.None);
        }

        public void SetByForm(
            Context context,
            SiteSettings ss,
            Dictionary<string, string> formData)
        {
            formData.ForEach(data =>
            {
                var key = data.Key;
                var value = data.Value ?? string.Empty;
                switch (key)
                {
                    case "Tenants_TenantName": TenantName = value.ToString(); break;
                    case "Tenants_Title": Title = new Title(TenantId, value); break;
                    case "Tenants_Body": Body = value.ToString(); break;
                    case "Tenants_ContractDeadline": ContractDeadline = value.ToDateTime().ToUniversal(context: context); break;
                    case "Tenants_DisableAllUsersPermission": DisableAllUsersPermission = value.ToBool(); break;
                    case "Tenants_DisableStartGuide": DisableStartGuide = value.ToBool(); break;
                    case "Tenants_LogoType": LogoType = (LogoTypes)value.ToInt(); break;
                    case "Tenants_HtmlTitleTop": HtmlTitleTop = value.ToString(); break;
                    case "Tenants_HtmlTitleSite": HtmlTitleSite = value.ToString(); break;
                    case "Tenants_HtmlTitleRecord": HtmlTitleRecord = value.ToString(); break;
                    case "Tenants_TopStyle": TopStyle = value.ToString(); break;
                    case "Tenants_TopScript": TopScript = value.ToString(); break;
                    case "Tenants_Timestamp": Timestamp = value.ToString(); break;
                    case "Comments": Comments.Prepend(
                        context: context,
                        ss: ss,
                        body: value); break;
                    case "VerUp": VerUp = value.ToBool(); break;
                    default:
                        if (key.RegexExists("Comment[0-9]+"))
                        {
                            Comments.Update(
                                context: context,
                                ss: ss,
                                commentId: key.Substring("Comment".Length).ToInt(),
                                body: value);
                        }
                        else
                        {
                            var column = ss.GetColumn(
                                context: context,
                                columnName: key.Split_2nd('_'));
                            switch (Def.ExtendedColumnTypes.Get(column?.ColumnName))
                            {
                                case "Class":
                                    Class(
                                        columnName: column.ColumnName,
                                        value: value);
                                    break;
                                case "Num":
                                    Num(
                                        columnName: column.ColumnName,
                                        value: new Num(
                                            context: context,
                                            column: column,
                                            value: value));
                                    break;
                                case "Date":
                                    Date(
                                        columnName: column.ColumnName,
                                        value: value.ToDateTime().ToUniversal(context: context));
                                    break;
                                case "Description":
                                    Description(
                                        columnName: column.ColumnName,
                                        value: value);
                                    break;
                                case "Check":
                                    Check(
                                        columnName: column.ColumnName,
                                        value: value.ToBool());
                                    break;
                                case "Attachments":
                                    Attachments(
                                        columnName: column.ColumnName,
                                        value: value.Deserialize<Attachments>());
                                    break;
                            }
                        }
                        break;
                }
            });
            if (context.QueryStrings.ContainsKey("ver"))
            {
                Ver = context.QueryStrings.Int("ver");
            }
            if (context.Action == "deletecomment")
            {
                DeleteCommentId = formData.Get("ControlId")?
                    .Split(',')
                    ._2nd()
                    .ToInt() ?? 0;
                Comments.RemoveAll(o => o.CommentId == DeleteCommentId);
            }
        }

        public void SetByModel(TenantModel tenantModel)
        {
            TenantName = tenantModel.TenantName;
            Title = tenantModel.Title;
            Body = tenantModel.Body;
            ContractSettings = tenantModel.ContractSettings;
            ContractDeadline = tenantModel.ContractDeadline;
            DisableAllUsersPermission = tenantModel.DisableAllUsersPermission;
            DisableStartGuide = tenantModel.DisableStartGuide;
            LogoType = tenantModel.LogoType;
            HtmlTitleTop = tenantModel.HtmlTitleTop;
            HtmlTitleSite = tenantModel.HtmlTitleSite;
            HtmlTitleRecord = tenantModel.HtmlTitleRecord;
            TopStyle = tenantModel.TopStyle;
            TopScript = tenantModel.TopScript;
            Comments = tenantModel.Comments;
            Creator = tenantModel.Creator;
            Updator = tenantModel.Updator;
            CreatedTime = tenantModel.CreatedTime;
            UpdatedTime = tenantModel.UpdatedTime;
            VerUp = tenantModel.VerUp;
            Comments = tenantModel.Comments;
            ClassHash = tenantModel.ClassHash;
            NumHash = tenantModel.NumHash;
            DateHash = tenantModel.DateHash;
            DescriptionHash = tenantModel.DescriptionHash;
            CheckHash = tenantModel.CheckHash;
            AttachmentsHash = tenantModel.AttachmentsHash;
        }

        public void SetByApi(Context context, SiteSettings ss)
        {
            var data = context.RequestDataString.Deserialize<TenantApiModel>();
            if (data == null)
            {
                context.InvalidJsonData = !context.RequestDataString.IsNullOrEmpty();
                return;
            }
            if (data.TenantName != null) TenantName = data.TenantName.ToString().ToString();
            if (data.Title != null) Title = new Title(data.Title.ToString());
            if (data.Body != null) Body = data.Body.ToString().ToString();
            if (data.ContractDeadline != null) ContractDeadline = data.ContractDeadline.ToDateTime().ToDateTime().ToUniversal(context: context);
            if (data.DisableAllUsersPermission != null) DisableAllUsersPermission = data.DisableAllUsersPermission.ToBool().ToBool();
            if (data.DisableStartGuide != null) DisableStartGuide = data.DisableStartGuide.ToBool().ToBool();
            if (data.LogoType != null) LogoType = (LogoTypes)data.LogoType.ToInt().ToInt();
            if (data.HtmlTitleTop != null) HtmlTitleTop = data.HtmlTitleTop.ToString().ToString();
            if (data.HtmlTitleSite != null) HtmlTitleSite = data.HtmlTitleSite.ToString().ToString();
            if (data.HtmlTitleRecord != null) HtmlTitleRecord = data.HtmlTitleRecord.ToString().ToString();
            if (data.TopStyle != null) TopStyle = data.TopStyle.ToString().ToString();
            if (data.TopScript != null) TopScript = data.TopScript.ToString().ToString();
            if (data.Comments != null) Comments.Prepend(context: context, ss: ss, body: data.Comments);
            if (data.VerUp != null) VerUp = data.VerUp.ToBool();
            data.ClassHash?.ForEach(o => Class(
                columnName: o.Key,
                value: o.Value));
            data.NumHash?.ForEach(o => Num(
                columnName: o.Key,
                value: new Num(
                    context: context,
                    column: ss.GetColumn(
                        context: context,
                        columnName: o.Key),
                    value: o.Value.ToString())));
            data.DateHash?.ForEach(o => Date(
                columnName: o.Key,
                value: o.Value.ToDateTime().ToUniversal(context: context)));
            data.DescriptionHash?.ForEach(o => Description(
                columnName: o.Key,
                value: o.Value));
            data.CheckHash?.ForEach(o => Check(
                columnName: o.Key,
                value: o.Value));
            data.AttachmentsHash?.ForEach(o =>
            {
                string columnName = o.Key;
                Attachments newAttachments = o.Value;
                Attachments oldAttachments;
                if (columnName == "Attachments#Uploading")
                {
                    var kvp = AttachmentsHash
                        .FirstOrDefault(x => x.Value
                            .Any(att => att.Guid == newAttachments.FirstOrDefault()?.Guid?.Split_1st()));
                    columnName = kvp.Key;
                    oldAttachments = kvp.Value;
                    var column = ss.GetColumn(
                        context: context,
                        columnName: columnName);
                    if (column.OverwriteSameFileName == true)
                    {
                        var oldAtt = oldAttachments
                            .FirstOrDefault(att => att.Guid == newAttachments.FirstOrDefault()?.Guid?.Split_1st());
                        if(oldAtt != null)
                        {
                            oldAtt.Deleted = true;
                            oldAtt.Overwritten = true;
                        }
                    }
                    newAttachments.ForEach(att => att.Guid = att.Guid.Split_2nd());
                }
                else
                {
                    oldAttachments = AttachmentsHash.Get(columnName);
                }
                if (oldAttachments != null)
                {
                    var column = ss.GetColumn(
                        context: context,
                        columnName: columnName);
                    var newGuidSet = new HashSet<string>(newAttachments.Select(x => x.Guid).Distinct());
                    var newNameSet = new HashSet<string>(newAttachments.Select(x => x.Name).Distinct());
                    if (column.OverwriteSameFileName == true)
                    {
                        newAttachments.AddRange(oldAttachments.
                            Where((oldvalue) =>
                                !newGuidSet.Contains(oldvalue.Guid) &&
                                !newNameSet.Contains(oldvalue.Name)));
                    }
                    else
                    {
                        newAttachments.AddRange(oldAttachments.
                            Where((oldvalue) => !newGuidSet.Contains(oldvalue.Guid)));
                    }
                }
                Attachments(columnName: columnName, value: newAttachments);
            });
        }

        private void SetBySession(Context context)
        {
        }

        private void Set(Context context, SiteSettings ss, DataTable dataTable)
        {
            switch (dataTable.Rows.Count)
            {
                case 1: Set(context, ss, dataTable.Rows[0]); break;
                case 0: AccessStatus = Databases.AccessStatuses.NotFound; break;
                default: AccessStatus = Databases.AccessStatuses.Overlap; break;
            }
        }

        private void Set(Context context, SiteSettings ss, DataRow dataRow, string tableAlias = null)
        {
            AccessStatus = Databases.AccessStatuses.Selected;
            foreach(DataColumn dataColumn in dataRow.Table.Columns)
            {
                var column = new ColumnNameInfo(dataColumn.ColumnName);
                if (column.TableAlias == tableAlias)
                {
                    switch (column.Name)
                    {
                        case "TenantId":
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                TenantId = dataRow[column.ColumnName].ToInt();
                                SavedTenantId = TenantId;
                            }
                            break;
                        case "Ver":
                            Ver = dataRow[column.ColumnName].ToInt();
                            SavedVer = Ver;
                            break;
                        case "TenantName":
                            TenantName = dataRow[column.ColumnName].ToString();
                            SavedTenantName = TenantName;
                            break;
                        case "Title":
                            Title = new Title(dataRow, "TenantId");
                            SavedTitle = Title.Value;
                            break;
                        case "Body":
                            Body = dataRow[column.ColumnName].ToString();
                            SavedBody = Body;
                            break;
                        case "ContractSettings":
                            ContractSettings = GetContractSettings(dataRow);
                            SavedContractSettings = ContractSettings?.RecordingJson();
                            break;
                        case "ContractDeadline":
                            ContractDeadline = dataRow[column.ColumnName].ToDateTime();
                            SavedContractDeadline = ContractDeadline;
                            break;
                        case "DisableAllUsersPermission":
                            DisableAllUsersPermission = dataRow[column.ColumnName].ToBool();
                            SavedDisableAllUsersPermission = DisableAllUsersPermission;
                            break;
                        case "DisableStartGuide":
                            DisableStartGuide = dataRow[column.ColumnName].ToBool();
                            SavedDisableStartGuide = DisableStartGuide;
                            break;
                        case "LogoType":
                            LogoType = (LogoTypes)dataRow[column.ColumnName].ToInt();
                            SavedLogoType = LogoType.ToInt();
                            break;
                        case "HtmlTitleTop":
                            HtmlTitleTop = dataRow[column.ColumnName].ToString();
                            SavedHtmlTitleTop = HtmlTitleTop;
                            break;
                        case "HtmlTitleSite":
                            HtmlTitleSite = dataRow[column.ColumnName].ToString();
                            SavedHtmlTitleSite = HtmlTitleSite;
                            break;
                        case "HtmlTitleRecord":
                            HtmlTitleRecord = dataRow[column.ColumnName].ToString();
                            SavedHtmlTitleRecord = HtmlTitleRecord;
                            break;
                        case "TopStyle":
                            TopStyle = dataRow[column.ColumnName].ToString();
                            SavedTopStyle = TopStyle;
                            break;
                        case "TopScript":
                            TopScript = dataRow[column.ColumnName].ToString();
                            SavedTopScript = TopScript;
                            break;
                        case "Comments":
                            Comments = dataRow[column.ColumnName].ToString().Deserialize<Comments>() ?? new Comments();
                            SavedComments = Comments.ToJson();
                            break;
                        case "Creator":
                            Creator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedCreator = Creator.Id;
                            break;
                        case "Updator":
                            Updator = SiteInfo.User(context: context, userId: dataRow.Int(column.ColumnName));
                            SavedUpdator = Updator.Id;
                            break;
                        case "CreatedTime":
                            CreatedTime = new Time(context, dataRow, column.ColumnName);
                            SavedCreatedTime = CreatedTime.Value;
                            break;
                        case "UpdatedTime":
                            UpdatedTime = new Time(context, dataRow, column.ColumnName); Timestamp = dataRow.Field<DateTime>(column.ColumnName).ToString("yyyy/M/d H:m:s.fff");
                            SavedUpdatedTime = UpdatedTime.Value;
                            break;
                        case "IsHistory":
                            VerType = dataRow.Bool(column.ColumnName)
                                ? Versions.VerTypes.History
                                : Versions.VerTypes.Latest; break;
                        default:
                            switch (Def.ExtendedColumnTypes.Get(column.Name))
                            {
                                case "Class":
                                    Class(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString());
                                    SavedClass(
                                        columnName: column.Name,
                                        value: Class(columnName: column.Name));
                                    break;
                                case "Num":
                                    Num(
                                        columnName: column.Name,
                                        value: new Num(
                                            dataRow: dataRow,
                                            name: column.ColumnName));
                                    SavedNum(
                                        columnName: column.Name,
                                        value: Num(columnName: column.Name).Value);
                                    break;
                                case "Date":
                                    Date(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToDateTime());
                                    SavedDate(
                                        columnName: column.Name,
                                        value: Date(columnName: column.Name));
                                    break;
                                case "Description":
                                    Description(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString());
                                    SavedDescription(
                                        columnName: column.Name,
                                        value: Description(columnName: column.Name));
                                    break;
                                case "Check":
                                    Check(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToBool());
                                    SavedCheck(
                                        columnName: column.Name,
                                        value: Check(columnName: column.Name));
                                    break;
                                case "Attachments":
                                    Attachments(
                                        columnName: column.Name,
                                        value: dataRow[column.ColumnName].ToString()
                                            .Deserialize<Attachments>() ?? new Attachments());
                                    SavedAttachments(
                                        columnName: column.Name,
                                        value: Attachments(columnName: column.Name).ToJson());
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        public bool Updated(Context context)
        {
            return Updated()
                || TenantId_Updated(context: context)
                || Ver_Updated(context: context)
                || TenantName_Updated(context: context)
                || Title_Updated(context: context)
                || Body_Updated(context: context)
                || ContractSettings_Updated(context: context)
                || ContractDeadline_Updated(context: context)
                || DisableAllUsersPermission_Updated(context: context)
                || DisableStartGuide_Updated(context: context)
                || LogoType_Updated(context: context)
                || HtmlTitleTop_Updated(context: context)
                || HtmlTitleSite_Updated(context: context)
                || HtmlTitleRecord_Updated(context: context)
                || TopStyle_Updated(context: context)
                || TopScript_Updated(context: context)
                || Comments_Updated(context: context)
                || Creator_Updated(context: context)
                || Updator_Updated(context: context);
        }

        public override List<string> Mine(Context context)
        {
            if (MineCache == null)
            {
                var mine = new List<string>();
                var userId = context.UserId;
                if (SavedCreator == userId) mine.Add("Creator");
                if (SavedUpdator == userId) mine.Add("Updator");
                MineCache = mine;
            }
            return MineCache;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private ContractSettings GetContractSettings(DataRow dataRow)
        {
            return null;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public enum LogoTypes
        {
            ImageOnly,
            ImageAndTitle
        }
    }
}
