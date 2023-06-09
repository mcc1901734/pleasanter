﻿using Implem.DefinitionAccessor;
using Implem.Pleasanter.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Implem.Pleasanter.Libraries.BackgroundServices
{
    public class SyncByLdapExecutionTimer : ExecutionTimerBase
    {
        override public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                var context = CreateContext();
                var log = CreateSysLogModel(
                    context: context,
                    message: "Execute SyncByLdap().");
                var json = UserUtilities.SyncByLdap(context: context);
                log.Finish(
                    context: context,
                    responseSize: json.Length);
            }, stoppingToken);
        }

        override public IList<string> GetTimeList()
        {
            return Parameters.BackgroundService.SyncByLdapTime;
        }

        public override bool Enabled()
        {
            return Parameters.BackgroundService.SyncByLdap;
        }
    }
}
