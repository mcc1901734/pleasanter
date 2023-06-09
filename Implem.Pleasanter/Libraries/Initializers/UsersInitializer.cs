﻿using Implem.DefinitionAccessor;
using Implem.Libraries.Utilities;
using Implem.Pleasanter.Libraries.DataSources;
using Implem.Pleasanter.Libraries.DataTypes;
using Implem.Pleasanter.Libraries.Requests;
using System;
namespace Implem.Pleasanter.Libraries.Initializers
{
    public static class UsersInitializer
    {
        public static void Initialize(Context context)
        {
            if (Repository.ExecuteScalar_int(
                context: context,
                statements: Rds.SelectUsers(
                    column: Rds.UsersColumn().UsersCount())) == 0)
            {
                var passwordExpirationTime = !Parameters.Service.WithoutChangeDefaultPassword
                    ? new Time(
                        context: context,
                        value: DateTime.Now)
                    : Parameters.Security.PasswordExpirationPeriod > 0
                        ? new Time(
                            context: context,
                            value: DateTime.Today.AddDays(Parameters.Security.PasswordExpirationPeriod))
                        : null;
                Create(
                    context: context,
                    tenantId: 1,
                    loginId: "Administrator",
                    name: "Administrator",
                    password: Parameters.Service.DefaultPassword.Sha512Cng(),
                    passwordExpirationTime: passwordExpirationTime,
                    tenantManager: true);
            }
        }

        private static void Create(
            Context context,
            int tenantId,
            string loginId,
            string name,
            bool disabled = false,
            string password = "",
            Time passwordExpirationTime = null,
            bool tenantManager = false)
        {
            Repository.ExecuteNonQuery(
                context: context,
                statements: Rds.InsertUsers(
                    param: Rds.UsersParam()
                        .TenantId(tenantId)
                        .LoginId(loginId)
                        .Disabled(disabled)
                        .Password(password)
                        .Name(name)
                        .DeptId(0)
                        .FirstAndLastNameOrder(1)
                        .PasswordExpirationTime(
                        context.Sqls.DateTimeValue(
                            value: passwordExpirationTime?.ToString()),
                            _using: passwordExpirationTime != null)
                        .TenantManager(tenantManager)));
        }
    }
}