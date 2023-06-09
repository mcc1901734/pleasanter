﻿using Implem.DefinitionAccessor;
using Implem.Pleasanter.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Implem.Pleasanter.Libraries.BackgroundServices
{
    public class DeleteSysLogsTimer : ExecutionTimerBase
    {
        override public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                var context = CreateContext();
                var log = CreateSysLogModel(
                    context: context,
                    message: "Delete SysLogs.");
                if (Parameters.SysLog.RetentionPeriod > 0)
                {
                    SysLogUtilities.PhysicalDelete(context);
                }
                log.Finish(context: context);
            }, stoppingToken);
        }

        override public IList<string> GetTimeList()
        {
            return Parameters.BackgroundService.DeleteSysLogsTime;
        }

        public override bool Enabled()
        {
            return Parameters.BackgroundService.DeleteSysLogs;
        }
    }
}
