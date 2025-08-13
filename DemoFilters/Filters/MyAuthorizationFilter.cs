using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoFilters.Filters
{
    /*
     * Apply Global Configuration
     * ServiceFilter
     * TypeFilter
     */
    //Authorization Filter
    public class MyAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("Sync - On Authorization");
        }
    }

    //Resource Filter
    public class MyResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Resource Filter - On Executed");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Resource Filter - On Executing");
        }
    }

    ////Action Filter - Globally, TypeFilter, ServiceFilter
    //public class MyActionFilter : IActionFilter
    //{
    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        Console.WriteLine("Action Filter - On Executed");
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        Console.WriteLine("Action Filter - On Executing");
    //    }
    //}

    //Called by using [MyActionFilter] on COntroller or Action
    //public class MyActionFilterAttribute(string name) : Attribute, IActionFilter
    //{

    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {
    //        Console.WriteLine($"Action Filter - On Executed - {name}");
    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        Console.WriteLine($"Action Filter - On Executing - {name}");
    //    }
    //}

    //Action Filter - Globally, TypeFilter, ServiceFilter - 

    public class MyActionFilter : IActionFilter
    {
        public string Name { get; set; }

        public MyActionFilter(string name)
        {
            Name = name;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Action Filter - On Executed -  {Name}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Action Filter - On Executing - {Name}");
        }
    }

    //Exception Filter
    public class MyExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("Exception Filter - On Executed");
        }
    }

    //Result Filter
    public class MyResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Result Filter - On Executed");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Result Filter - On Executing");
        }
    }

    public class MyActionAsyncFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"Before");
            await next();
            Console.WriteLine("After");
        }
    }
}
