﻿using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Libraries.Security;
using Implem.Pleasanter.Models;
using System.Web.Mvc;
namespace Implem.Pleasanter.Filters
{
    public class CheckUserAttributes : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var context = new Context();
            if (!context.LoginId.IsNullOrEmpty())
            {
                var userModel = new UserModel().Get(
                    context: context,
                    ss: null,
                    where: Rds.UsersWhere()
                        .TenantId(context.TenantId)
                        .UserId(context.UserId)
                        .Disabled(0));
                if (userModel.AccessStatus != Databases.AccessStatuses.Selected)
                {
                    if (Authentications.Windows())
                    {
                        filterContext.Result = new EmptyResult();
                    }
                    else
                    {
                        Authentications.SignOut();
                        filterContext.Result = new RedirectResult(Locations.Login());
                    }
                }
            }
        }
    }
}