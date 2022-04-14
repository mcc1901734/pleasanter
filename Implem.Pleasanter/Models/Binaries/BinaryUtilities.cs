﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Classes;
using Implem.Libraries.DataSources.Interfaces;
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
using Implem.Pleasanter.Libraries.Resources;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Libraries.Server;
using Implem.Pleasanter.Libraries.Settings;
using Implem.Pleasanter.Libraries.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static Implem.Pleasanter.Libraries.ServerScripts.ServerScriptModel;
namespace Implem.Pleasanter.Models
{
    public static class BinaryUtilities
    {
        /// <summary>
        /// Fixed:
        /// </summary>
        public static bool ExistsSiteImage(
            Context context,
            SiteSettings ss,
            long referenceId,
            Libraries.Images.ImageData.SizeTypes sizeType)
        {
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: ss);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return false;
            }
            switch (Parameters.BinaryStorage.GetSiteImageProvider())
            {
                case "Local":
                    return new Libraries.Images.ImageData(
                        referenceId, Libraries.Images.ImageData.Types.SiteImage)
                            .Exists(sizeType);
                default:
                    return Repository.ExecuteScalar_int(
                        context: context,
                        statements: Rds.SelectBinaries(
                            column: Rds.BinariesColumn().BinariesCount(),
                            where: Rds.BinariesWhere()
                                .ReferenceId(referenceId)
                                .BinaryType("SiteImage"))) == 1;
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static bool ExistsTenantImage(
            Context context,
            SiteSettings ss,
            long referenceId,
            Libraries.Images.ImageData.SizeTypes sizeType)
        {
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: ss);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return false;
            }
            switch (Parameters.BinaryStorage.Provider)
            {
                case "Local":
                    return new Libraries.Images.ImageData(
                        referenceId, Libraries.Images.ImageData.Types.TenantImage)
                            .Exists(sizeType);
                default:
                    return Repository.ExecuteScalar_int(
                        context: context,
                        statements: Rds.SelectBinaries(
                            column: Rds.BinariesColumn().BinariesCount(),
                            where: Rds.BinariesWhere()
                                .ReferenceId(referenceId)
                                .BinaryType("TenantImage"))) == 1;
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string SiteImagePrefix(
            Context context,
            SiteSettings ss,
            long referenceId,
            Libraries.Images.ImageData.SizeTypes sizeType)
        {
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: ss);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return string.Empty;
            }
            return new BinaryModel(referenceId).SiteImagePrefix(
                context: context,
                sizeType: sizeType);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string TenantImagePrefix(
            Context context,
            SiteSettings ss,
            long referenceId,
            Libraries.Images.ImageData.SizeTypes sizeType)
        {
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: ss);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return string.Empty;
            }
            return new BinaryModel(referenceId).TenantImagePrefix(
                context: context,
                sizeType: sizeType);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static (byte[] bytes, string contentType) SiteImageThumbnail(Context context, SiteModel siteModel)
        {
            siteModel.SiteSettings = SiteSettingsUtilities.Get(
                context: context,
                siteModel: siteModel,
                referenceId: siteModel.SiteId);
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: siteModel.SiteSettings);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return (null, null);
            }
            var binaryModel = new BinaryModel(
                context: context,
                referenceId: siteModel.SiteId,
                binaryType: "SiteImage");
            return (
                binaryModel.SiteImage(
                    context: context,
                    sizeType: Libraries.Images.ImageData.SizeTypes.Thumbnail,
                    column: Rds.BinariesColumn().Thumbnail()),
                binaryModel.ContentType.IsNullOrEmpty()
                    ? "image/bmp"
                    : binaryModel.ContentType);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static (byte[] bytes, string contentType) SiteImageIcon(Context context, SiteModel siteModel)
        {
            siteModel.SiteSettings = SiteSettingsUtilities.Get(
                context: context,
                siteModel: siteModel,
                referenceId: siteModel.SiteId);
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: siteModel.SiteSettings);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return (null, null);
            }
            var binaryModel = new BinaryModel(
                context: context,
                referenceId: siteModel.SiteId,
                binaryType: "SiteImage");
            return (
                binaryModel.SiteImage(
                    context: context,
                    sizeType: Libraries.Images.ImageData.SizeTypes.Icon,
                    column: Rds.BinariesColumn().Icon()),
                binaryModel.ContentType.IsNullOrEmpty()
                    ? "image/bmp"
                    : binaryModel.ContentType);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static (byte[] bytes, string contentType) TenantImageLogo(Context context, TenantModel tenantModel)
        {
            var ss = SiteSettingsUtilities.TenantsSiteSettings(context);
            var invalid = BinaryValidators.OnGetting(
                context: context,
                ss: ss);
            switch (invalid.Type)
            {
                case Error.Types.None: break;
                default: return (null, null);
            }
            var binaryModel = new BinaryModel(
                context: context,
                referenceId: tenantModel.TenantId,
                binaryType: "TenantImage");
            return (
                binaryModel.TenantImage(
                    context: context,
                    sizeType: Libraries.Images.ImageData.SizeTypes.Logo,
                    column: Rds.BinariesColumn().Bin()),
                binaryModel.ContentType.IsNullOrEmpty()
                    ? "image/bmp"
                    : binaryModel.ContentType);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string UpdateSiteImage(Context context, SiteModel siteModel)
        {
            siteModel.SiteSettings = SiteSettingsUtilities.Get(
                context: context,
                siteModel: siteModel,
                referenceId: siteModel.SiteId);
            var bin = context.PostedFiles.FirstOrDefault()?.Byte();
            var invalid = BinaryValidators.OnUploadingSiteImage(
                context: context,
                ss: siteModel.SiteSettings,
                bin: bin);
            switch (invalid)
            {
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var error = new BinaryModel(siteModel.SiteId).UpdateSiteImage(
                context: context,
                ss: siteModel.SiteSettings,
                bin: bin);
            if (error.Has())
            {
                return error.MessageJson(context: context);
            }
            else
            {
                return new ResponseCollection()
                    .Html(
                        "#TenantImageLogoContainer",
                        new HtmlBuilder().SiteImageIcon(
                            context: context,
                            ss: siteModel.SiteSettings,
                            siteId: siteModel.TenantId))
                    .Html(
                        "#TenantImageSettingsEditor",
                        new HtmlBuilder().SiteImageSettingsEditor(
                            context: context,
                            ss: siteModel.SiteSettings))
                    .Message(Messages.FileUpdateCompleted(context: context))
                    .ToJson();
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string UpdateTenantImage(Context context, TenantModel tenantModel)
        {
            var ss = SiteSettingsUtilities.TenantsSiteSettings(context);
            var bin = context.PostedFiles.FirstOrDefault()?.Byte();
            var invalid = BinaryValidators.OnUploadingTenantImage(
                context: context,
                ss: ss,
                bin: bin);
            switch (invalid)
            {
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var error = new BinaryModel(tenantModel.TenantId).UpdateTenantImage(
                context: context,
                ss: ss,
                bin: bin);
            if (error.Has())
            {
                return error.MessageJson(context: context);
            }
            else
            {
                return new ResponseCollection()
                   .ReplaceAll(
                       "#Logo",
                       new HtmlBuilder().HeaderLogo(
                           context: context,
                           ss: ss))
                   .ReplaceAll(
                       "#TenantImageSettingsEditor",
                       new HtmlBuilder().TenantImageSettingsEditor(
                           context: context, tenantModel: tenantModel))
                   .Message(Messages.FileUpdateCompleted(context: context))
                   .ToJson();
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string DeleteSiteImage(Context context, SiteModel siteModel)
        {
            siteModel.SiteSettings = SiteSettingsUtilities.Get(
                context: context,
                siteModel: siteModel,
                referenceId: siteModel.SiteId);
            var invalid = BinaryValidators.OnDeletingSiteImage(
                context: context,
                ss: siteModel.SiteSettings);
            switch (invalid)
            {
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var error = new BinaryModel(siteModel.SiteId)
                .DeleteSiteImage(context: context);
            if (error.Has())
            {
                return error.MessageJson(context: context);
            }
            else
            {
                return new ResponseCollection()
                    .Html(
                        "#SiteImageIconContainer",
                        new HtmlBuilder().SiteImageIcon(
                            context: context,
                            ss: siteModel.SiteSettings,
                            siteId: siteModel.SiteId))
                    .Html(
                        "#SiteImageSettingsEditor",
                        new HtmlBuilder().SiteImageSettingsEditor(
                            context: context,
                            ss: siteModel.SiteSettings))
                    .Message(Messages.FileDeleteCompleted(context: context))
                    .ToJson();
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string DeleteTenantImage(Context context, TenantModel tenantModel)
        {
            var ss = SiteSettingsUtilities.TenantsSiteSettings(context);
            var invalid = BinaryValidators.OnDeletingTenantImage(
                context: context,
                ss: ss);
            switch (invalid)
            {
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var error = new BinaryModel(tenantModel.TenantId)
                .DeleteTenantImage(context: context);
            if (error.Has())
            {
                return error.MessageJson(context: context);
            }
            else
            {
                return new ResponseCollection()
                   .ReplaceAll(
                       "#Logo",
                       new HtmlBuilder().HeaderLogo(
                           context: context,
                           ss: ss))
                   .ReplaceAll(
                       "#TenantImageSettingsEditor",
                       new HtmlBuilder().TenantImageSettingsEditor(
                           context: context, tenantModel: tenantModel))
                   .Message(Messages.FileDeleteCompleted(context: context))
                   .ToJson();
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string UploadImage(Context context, long id)
        {
            var invalid = BinaryValidators.OnUploadingImage(context: context);
            switch (invalid)
            {
                case Error.Types.OverTenantStorageSize:
                    return Messages.ResponseOverTenantStorageSize(
                        context: context,
                        data: context.ContractSettings.StorageSize.ToString()).ToJson();
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var file = context.PostedFiles.FirstOrDefault();
            var bin = file.Byte();
            var columnName = context.Forms.Data("ControlId");
            if (columnName.Contains("_"))
            {
                columnName = columnName.Substring(columnName.IndexOf("_") + 1);
            }
            if (columnName.StartsWith("Comment"))
            {
                columnName = "Comments";
            }
            var ss = new ItemModel(
                context: context,
                referenceId: id)
                    .GetSite(
                        context: context,
                        initSiteSettings: true)
                            .SiteSettings;
            var thumbnailLimitSize = ss.GetColumn(
                context: context,
                columnName: columnName)?.ThumbnailLimitSize
                    ?? Parameters.BinaryStorage.ThumbnailLimitSize;
            var imageData = new Libraries.Images.ImageData(
                bin,
                ss.ReferenceId,
                Libraries.Images.ImageData.Types.SiteImage);
            if (Parameters.BinaryStorage.ImageLimitSize?.ToInt() > 0)
            {
                bin = imageData.ReSizeBytes(Parameters.BinaryStorage.ImageLimitSize);
            }
            var thumbnail = thumbnailLimitSize > 0
                ? imageData.ReSizeBytes(thumbnailLimitSize)
                : null;
            if (Parameters.BinaryStorage.IsLocal())
            {
                bin.Write(System.IO.Path.Combine(
                    Directories.BinaryStorage(),
                    "Images",
                    file.Guid));
                if (thumbnailLimitSize > 0)
                {
                    thumbnail.Write(System.IO.Path.Combine(
                        Directories.BinaryStorage(),
                        "Images",
                        file.Guid + "_thumbnail"));
                }
            }
            Repository.ExecuteNonQuery(
                context: context,
                statements: Rds.InsertBinaries(
                    param: Rds.BinariesParam()
                        .TenantId(context.TenantId)
                        .ReferenceId(id)
                        .Guid(file.Guid)
                        .BinaryType("Images")
                        .Title(file.FileName)
                        .Bin(bin, _using: !Parameters.BinaryStorage.IsLocal())
                        .Thumbnail(thumbnail, _using: thumbnail != null)
                        .FileName(file.FileName)
                        .Extension(file.Extension)
                        .Size(file.Size)
                        .ContentType(file.ContentType)));
            return new ResponseCollection()
                .InsertText(
                    "#" + context.Forms.ControlId(),
                    "![image]({0})".Params(Locations.ShowFile(
                        context: context,
                        guid: file.Guid)))
                .ToJson();
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string DeleteImage(Context context, string guid)
        {
            var binaryModel = new BinaryModel()
                .Get(
                    context: context,
                    where: Rds.BinariesWhere()
                        .TenantId(context.TenantId)
                        .Guid(guid.ToUpper()));
            var ss = new ItemModel(
                context: context,
                referenceId: binaryModel.ReferenceId)
                    .GetSite(
                        context: context,
                        initSiteSettings: true)
                            .SiteSettings;
            var invalid = BinaryValidators.OnDeletingImage(
                context: context,
                ss: ss,
                binaryModel: binaryModel);
            switch (invalid)
            {
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            binaryModel.Delete(context: context);
            return new ResponseCollection()
                .Message(Messages.DeletedImage(context: context))
                .Remove($"#ImageLib .item[data-id=\"{guid}\"]")
                .ToJson();
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string MultiUpload(Context context, long id)
        {
            var controlId = context.Forms.ControlId();
            var ss = new ItemModel(
                context: context,
                referenceId: id).GetSite(
                    context: context,
                    initSiteSettings: true)
                        .SiteSettings;
            var column = ss.GetColumn(
                context: context,
                columnName: context.Forms.Data("ColumnName"));
            var attachments = context.Forms.Data("AttachmentsData").Deserialize<Attachments>();
            context.PostedFiles.ForEach(file =>
            {
                if (column.OverwriteSameFileName == true)
                {
                    OverwriteSameFileName(attachments, file.FileName);
                }
                attachments.Add(new Attachment()
                {
                    Guid = file.Guid,
                    Name = file.FileName.Split(System.IO.Path.DirectorySeparatorChar).Last(),
                    Size = file.Size,
                    Extention = file.Extension,
                    ContentType = file.ContentType,
                    Added = true,
                    Deleted = false
                });
            });
            var invalid = BinaryValidators.OnUploading(
                context: context,
                column: column,
                attachments: attachments);
            switch (invalid)
            {
                case Error.Types.OverLimitQuantity:
                    return Messages.ResponseOverLimitQuantity(
                        context: context,
                        data: column.LimitQuantity.ToString()).ToJson();
                case Error.Types.OverLimitSize:
                    return Messages.ResponseOverLimitSize(
                        context: context,
                        data: column.LimitSize.ToString()).ToJson();
                case Error.Types.OverTotalLimitSize:
                    return Messages.ResponseOverTotalLimitSize(
                        context: context,
                        data: column.TotalLimitSize.ToString()).ToJson();
                case Error.Types.OverLocalFolderLimitSize:
                    return Messages.ResponseOverLocalFolderLimitSize(
                        context: context,
                        data: column.LocalFolderLimitSize.ToString()).ToJson();
                case Error.Types.OverLocalFolderTotalLimitSize:
                    return Messages.ResponseOverLocalFolderTotalLimitSize(
                        context: context,
                        data: column.LocalFolderTotalLimitSize.ToString()).ToJson();
                case Error.Types.OverTenantStorageSize:
                    return Messages.ResponseOverTenantStorageSize(
                        context: context,
                        data: context.ContractSettings.StorageSize.ToString()).ToJson();
                case Error.Types.None: break;
                default: return invalid.MessageJson(context: context);
            }
            var hb = new HtmlBuilder();
            var fieldId = controlId + "Field";
            return new ResponseCollection()
                .ReplaceAll("#" + fieldId, new HtmlBuilder()
                    .FieldAttachments(
                        context: context,
                        fieldId: fieldId,
                        controlId: controlId,
                        columnName: column.ColumnName,
                        fieldCss: column.FieldCss
                            + (column.TextAlign == SiteSettings.TextAlignTypes.Right
                                ? " right-align"
                                : string.Empty),
                        fieldDescription: column.Description,
                        labelText: column.LabelText,
                        value: attachments.ToJson(),
                        readOnly: Permissions.ColumnPermissionType(
                            context: context,
                            ss: ss,
                            column: column,
                            baseModel: null)
                                != Permissions.ColumnPermissionTypes.Update))
                .SetData("#" + controlId)
                .ToJson();
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static ResponseFile Donwload(Context context, string guid)
        {
            if (!context.ContractSettings.Attachments())
            {
                return null;
            }
            return FileContentResults.Download(context: context, guid: guid.ToUpper());
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static ContentResultInheritance ApiDonwload(Context context, string guid)
        {
            if (!Mime.ValidateOnApi(contentType: context.ContentType))
            {
                return ApiResults.BadRequest(context: context);
            }
            if (!context.ContractSettings.Attachments())
            {
                return null;
            }
            return FileContentResults.DownloadByApi(context: context, guid: guid.ToUpper());
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static ResponseFile DownloadTemp(Context context, string guid)
        {
            if (!context.ContractSettings.Attachments())
            {
                return null;
            }
            return FileContentResults.DownloadTemp(guid.ToUpper());
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string DeleteTemp(Context context)
        {
            if (!context.ContractSettings.Attachments())
            {
                return null;
            }
            Libraries.DataSources.File.DeleteTemp(context.Forms.Data("Guid"));
            return "[]";
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static decimal UsedTenantStorageSize(Context context)
        {
            return Repository.ExecuteScalar_decimal(
                context: context,
                statements: Rds.SelectBinaries(
                    column: Rds.BinariesColumn().Size(function: Sqls.Functions.Sum),
                    where: Rds.BinariesWhere().TenantId(context.TenantId)));
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static SqlStatement UpdateReferenceId(
            Context context, SiteSettings ss, long referenceId, string values)
        {
            var guids = values?.RegexValues("[0-9a-z]{32}").ToList().ConvertAll(x => x.ToUpper());
            return guids?.Any() == true
                ? Rds.UpdateBinaries(
                    param: Rds.BinariesParam().ReferenceId(referenceId),
                    where: Rds.BinariesWhere()
                        .TenantId(context.TenantId)
                        .ReferenceId(ss.SiteId)
                        .Guid(guids, multiParamOperator: " or ")
                        .Creator(context.UserId))
                : null;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string BinaryStorageProvider(Column column)
        {
            if (Parameters.BinaryStorage.UseStorageSelect)
            {
                return string.IsNullOrEmpty(column?.BinaryStorageProvider)
                    ? Parameters.BinaryStorage.DefaultBinaryStorageProvider
                    : column?.BinaryStorageProvider;
            }
            else
            {
                return Parameters.BinaryStorage.IsLocal()
                    ? "LocalFolder"
                    : "DataBase";
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string BinaryStorageProvider(Column column, long size)
        {
            decimal s = size;
            return BinaryStorageProvider(column, s);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static string BinaryStorageProvider(Column column, decimal size)
        {
            var binaryStorageProvider = BinaryStorageProvider(column);
            switch (binaryStorageProvider)
            {
                case "AutoDataBaseOrLocalFolder":
                    return size > column?.LimitSize * 1024M * 1024M
                        ? "LocalFolder"
                        : "DataBase";
                default:
                    return binaryStorageProvider;
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static System.Web.Mvc.ContentResult UploadFile(
            Context context,
            long id,
            HttpFileCollectionBase collectionBase)
        {
            var itemModel = new ItemModel(context, id);
            var ss = itemModel.GetSite(context, initSiteSettings: true).SiteSettings;
            var column = ss.GetColumn(context, TrimIdSuffix(HttpContext.Current.Request.Form["ColumnName"]));
            var attachments = HttpContext.Current.Request.Form["AttachmentsData"].Deserialize<Attachments>();
            var fileHash = HttpContext.Current.Request.Form["FileHash"];
            var files = ToArray(collectionBase);
            var contentRange = GetContentRange(files);
            {
                var invalid = HasPermission(context, ss, itemModel);
                switch (invalid.Type)
                {
                    case Error.Types.None: break;
                    default: return ApiResults.Get(HtmlTemplates.Error(context, invalid));
                }
            }
            {
                var invalid = BinaryValidators.OnUploading(context, column, attachments, files, new[] { contentRange });
                switch (invalid)
                {
                    case Error.Types.OverLimitQuantity:
                        return ApiResults.Get(Messages.ResponseOverLimitQuantity(
                            context: context,
                            data: column.LimitQuantity.ToString()).ToJson());
                    case Error.Types.OverLimitSize:
                        return ApiResults.Get(Messages.ResponseOverLimitSize(
                            context: context,
                            data: column.LimitSize.ToString()).ToJson());
                    case Error.Types.OverTotalLimitSize:
                        return ApiResults.Get(Messages.ResponseOverTotalLimitSize(
                            context: context,
                            data: column.TotalLimitSize.ToString()).ToJson());
                    case Error.Types.OverLocalFolderLimitSize:
                        return ApiResults.Get(Messages.ResponseOverLimitSize(
                            context: context,
                            data: column.LocalFolderLimitSize.ToString()).ToJson());
                    case Error.Types.OverLocalFolderTotalLimitSize:
                        return ApiResults.Get(Messages.ResponseOverTotalLimitSize(
                            context: context,
                            data: column.LocalFolderTotalLimitSize.ToString()).ToJson());
                    case Error.Types.OverTenantStorageSize:
                        return ApiResults.Get(Messages.ResponseOverTenantStorageSize(
                            context: context,
                            data: context.ContractSettings.StorageSize.ToString()).ToJson());
                    case Error.Types.None: break;
                    default: return ApiResults.Get(invalid.MessageJson(context));
                }
            }
            var controlId = HttpContext.Current.Request.Form["ControlId"];
            var fileUuid = HttpContext.Current.Request.Form["Uuid"]?.Split(',');
            var fileUuids = HttpContext.Current.Request.Form["Uuids"]?.Split(',');
            var fileNames = HttpContext.Current.Request.Form["fileNames"]?.Deserialize<string[]>();
            var fileSizes = HttpContext.Current.Request.Form["fileSizes"]?.Deserialize<string[]>();
            var fileTypes = HttpContext.Current.Request.Form["fileTypes"]?.Deserialize<string[]>();
            var resultFileNames = new List<KeyValuePair<HttpPostedFileBase, System.IO.FileInfo>>();
            for (int filesIndex = 0; filesIndex < collectionBase.Count; ++filesIndex)
            {
                var file = collectionBase[filesIndex];
                var saveFile = GetTempFileInfo(fileUuid[filesIndex], file.FileName);
                Save(file, saveFile);
                resultFileNames.Add(
                    new KeyValuePair<HttpPostedFileBase, System.IO.FileInfo>(
                        file,
                        saveFile));
            }
            {
                var invalid = ValidateFileHash(resultFileNames[0].Value, contentRange, fileHash);
                if (invalid != Error.Types.None) return ApiResults.Get(invalid.MessageJson(context));
            }
            return CreateResult(
                resultFileNames,
                CreateResponseJson(
                    context,
                    fileUuids,
                    fileNames,
                    fileSizes,
                    fileTypes,
                    ss,
                    column,
                    controlId,
                    attachments,
                    contentRange));
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static ErrorData HasPermission(Context context, SiteSettings ss, ItemModel itemModel)
        {
            if (ss.SiteId == ss.ReferenceId && itemModel.ReferenceType == "Sites")
                return context.CanCreate(ss)
                    ? new ErrorData(Error.Types.None)
                    : new ErrorData(Error.Types.HasNotPermission);
            switch (ss.ReferenceType)
            {
                case "Issues":
                    return IssueValidators.OnUpdating(
                        context: context,
                        ss: ss,
                        issueModel: new IssueModel(
                            context: context,
                            ss: ss,
                            issueId: context.Id));
                case "Results":
                    return ResultValidators.OnUpdating(
                        context: context,
                        ss: ss,
                        new ResultModel(
                            context: context,
                            ss: ss,
                            resultId: context.Id));
                default: return new ErrorData(Error.Types.HasNotPermission);
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static HttpPostedFileBase[] ToArray(HttpFileCollectionBase collectionBase)
        {
            var list = new List<HttpPostedFileBase>();
            for (int filesIndex = 0; filesIndex < HttpContext.Current.Request.Files.Count; ++filesIndex)
                list.Add(collectionBase[filesIndex]);
            return list.ToArray();
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static System.Net.Http.Headers.ContentRangeHeaderValue GetContentRange(
            HttpPostedFileBase[] files)
        {
            var contentRange = HttpContext.Current.Request.Headers["Content-Range"];
            var matches = System.Text.RegularExpressions.Regex.Matches(contentRange ?? string.Empty, "\\d+");
            return matches.Count > 0
                ? new System.Net.Http.Headers.ContentRangeHeaderValue(
                    long.Parse(matches[0].Value),
                    long.Parse(matches[1].Value),
                    long.Parse(matches[2].Value))
                : files.Sum(f => f.ContentLength) > 0
                    ? new System.Net.Http.Headers.ContentRangeHeaderValue(
                        0,
                        files.Select(f => f.ContentLength - 1).FirstOrDefault(),
                        files.Select(f => f.ContentLength).FirstOrDefault())
                    : new System.Net.Http.Headers.ContentRangeHeaderValue(0, 0, 0);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static System.IO.FileInfo GetTempFileInfo(string fileUuid, string fileName)
        {
            var tempDirectoryInfo = new System.IO.DirectoryInfo(DefinitionAccessor.Directories.Temp());
            if (!tempDirectoryInfo.Exists)
                tempDirectoryInfo.Create();
            var saveFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(tempDirectoryInfo.FullName, fileUuid, fileName));
            var saveDirectoryInfo = saveFileInfo.Directory;
            if (!saveDirectoryInfo.Exists)
                saveDirectoryInfo.Create();
            if (!saveFileInfo.Exists)
                using (var fileStream = saveFileInfo.Create())
                    fileStream.Flush();
            return saveFileInfo;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static void Save(HttpPostedFileBase file, System.IO.FileInfo saveFile)
        {
            System.IO.FileStream saveFileStream = null;
            var en = Enumerable.Range(0, 100).ToArray();
            foreach (var index in en)
            {
                try
                {
                    saveFileStream = saveFile.Open(System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Read);
                    if (saveFileStream != null) break;
                }
                catch (System.IO.IOException)
                {
                    if (index >= en.Last()) throw;
                }
            }
            using (saveFileStream)
            {
                file.InputStream.CopyTo(saveFileStream);
                saveFileStream.Flush();
            }
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static System.Web.Mvc.ContentResult CreateResult(
            List<KeyValuePair<HttpPostedFileBase, System.IO.FileInfo>> resultFileNames,
            string responseJson)
        {
            return new System.Web.Mvc.ContentResult
            {
                Content = Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    files = resultFileNames.Select(
                    file => new { name = file.Value.Name }).ToArray(),
                    ResponseJson = responseJson
                })
            };
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static string CreateResponseJson(
            Context context,
            IEnumerable<string> guids,
            IEnumerable<string> names,
            IEnumerable<string> sizes,
            IEnumerable<string> types,
            SiteSettings ss,
            Column column,
            string controlId,
            List<Attachment> attachments,
            System.Net.Http.Headers.ContentRangeHeaderValue contentRange)
        {
            Enumerable.Range(0, new[] { guids.Count(), names.Count(), sizes.Count(), types.Count() }.Min()).ForEach(index =>
            {
                var fileName = names.Skip(index).First();
                if (column.OverwriteSameFileName == true)
                {
                    OverwriteSameFileName(attachments, fileName);
                }
                attachments.Add(new Attachment()
                {
                    Guid = guids.Skip(index).First(),
                    Name = fileName,
                    Size = sizes.Skip(index).First().ToLong(),
                    Extention = System.IO.Path.GetExtension(names.Skip(index).First()),
                    ContentType = types.Skip(index).First(),
                    Added = true,
                    Deleted = false
                });
            });
            var hb = new HtmlBuilder();
            return new ResponseCollection()
                .ReplaceAll($"#{controlId}Field", new HtmlBuilder()
                    .Field(
                        context: context,
                        ss: ss,
                        column: column,
                        value: attachments.ToJson(),
                        columnPermissionType: Permissions.ColumnPermissionType(
                            context: context,
                            ss: ss,
                            column: column,
                            null),
                        idSuffix: System.Text.RegularExpressions.Regex.Match(controlId, "_\\d+_-?\\d+").Value
                        ))
                .SetData("#" + controlId)
                .ToJson();
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static Error.Types ValidateFileHash(
            System.IO.FileInfo fileInfo,
            System.Net.Http.Headers.ContentRangeHeaderValue contentRange,
            string hash)
        {
            if (string.IsNullOrEmpty(hash)) return Error.Types.None;
            if (contentRange.Length > (contentRange.To + 1)) return Error.Types.None;
            byte[] hashValue;
            using (var fileStream = fileInfo.Open(System.IO.FileMode.Open))
            {
                fileStream.Position = 0;
                hashValue = new System.Security.Cryptography.MD5Cng().ComputeHash(fileStream);
                fileStream.Close();
            }
            var fileHash = string.Join(string.Empty, hashValue.Select(h => h.ToString("x2")));
            return hash == fileHash ? Error.Types.None : Error.Types.InvalidRequest;
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        private static string TrimIdSuffix(string element)
        {
            var regex = new System.Text.RegularExpressions.Regex("_\\d+_-?\\d+$");
            return regex.Match(element).Value.IsNullOrEmpty()
                ? element
                : element.Replace(regex.Match(element).Value, string.Empty);
        }

        /// <summary>
        /// Fixed:
        /// </summary>
        public static void OverwriteSameFileName(List<Attachment> attachments, String fileName)
        {
            attachments.ForEach(savedAttachment =>
            {
                if (savedAttachment.Name == fileName)
                {
                    savedAttachment.Deleted = true;
                    savedAttachment.Overwritten = true;
                }
            });
        }
    }
}
