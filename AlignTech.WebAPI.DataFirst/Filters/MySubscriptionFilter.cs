using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AlignTech.WebAPI.DataFirst.Filters
{
    public class MySubscriptionFilter(QuickKartDbContext dbContext) : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            Guid.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId);

            if (userId == Guid.Empty)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userPlan = await dbContext.UserSubscriptions
                                    .Where(x => x.UserId == userId)
                                    .Select(x => x.SubscriptionPlan)
                                    .FirstOrDefaultAsync();
            if(userPlan != SubscriptionPlan.Premium)
            {
                context.Result = new ForbidResult();//403
            }
        }
    }
}
