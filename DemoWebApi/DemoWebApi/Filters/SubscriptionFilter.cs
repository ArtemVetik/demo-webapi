using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using DemoWebApi.Extentions;

namespace DemoWebApi.Filters
{
    public class SubscriptionFilter : ActionFilterAttribute
    {
        private readonly IRepositoryWrapper _repository;

        public SubscriptionFilter(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var playerId = context.HttpContext.User.GetUserId();

            if (string.IsNullOrEmpty(playerId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var subscription = _repository.Subscriptions.FindByCondition(data => data.player_id == playerId).FirstOrDefault();

            if (subscription == null || DateTime.UtcNow > subscription.expires_at)
            {
                context.Result = new BadRequestObjectResult("The user does not have a subscription");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
