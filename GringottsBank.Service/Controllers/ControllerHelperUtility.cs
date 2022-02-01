using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace GringottsBank.Service.Controllers
{
    public static class ControllerHelperUtility
    {
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;
            var errorList = query.ToList();
            return errorList;
        }
    }
}
