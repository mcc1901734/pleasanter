﻿using Implem.Pleasanter.Libraries.Requests;
using Implem.Pleasanter.Libraries.Responses;
using Implem.Pleasanter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Implem.Pleasanter.Controllers
{
    [AllowAnonymous]
    public class Api_ItemsController : Controller
    {
        [HttpPost]
        public ContentResult Get(long id)
        {
            var context = new Context();
            var log = new SysLogModel(context: context);
            var result = context.Authenticated
                ? new ItemModel(context: context, referenceId: id).GetByApi(context: context)
                : ApiResults.Unauthorized(context: context);
            log.Finish(context: context, responseSize: result.Content.Length);
            return result.ToHttpResponse(request: Request);
        }

        [HttpPost]
        public ContentResult Create(long id)
        {
            var context = new Context();
            var log = new SysLogModel(context: context);
            var result = context.Authenticated
                ? new ItemModel(context: context, referenceId: id).CreateByApi(context: context)
                : ApiResults.Unauthorized(context: context);
            log.Finish(context: context, responseSize: result.Content.Length);
            return result.ToHttpResponse(request: Request);
        }

        [HttpPost]
        public ContentResult Update(long id)
        {
            var context = new Context();
            var log = new SysLogModel(context: context);
            var result = context.Authenticated
                ? new ItemModel(context: context, referenceId: id).UpdateByApi(context: context)
                : ApiResults.Unauthorized(context: context);
            log.Finish(context: context, responseSize: result.Content.Length);
            return result.ToHttpResponse(request: Request);
        }

        [HttpPost]
        public ContentResult Delete(long id)
        {
            var context = new Context();
            var log = new SysLogModel(context: context);
            var result = context.Authenticated
                ? new ItemModel(context: context, referenceId: id).DeleteByApi(context: context)
                : ApiResults.Unauthorized(context: context);
            log.Finish(context: context, responseSize: result.Content.Length);
            return result.ToHttpResponse(request: Request);
        }
    }
}