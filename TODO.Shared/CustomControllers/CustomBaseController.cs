using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TODO.Shared.Dtos;

namespace TODO.Shared.CustomControllers
{
    public class CustomBaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult CreateActionValidateErrors()
        {
            Error validationError = new Error();

            ModelState.Values.ToList().ForEach(x =>
            {
                x.Errors.ToList().ForEach(y =>
                {
                    if (!string.IsNullOrEmpty(y.ErrorMessage))
                        validationError.Errors.Add(y.ErrorMessage);
                    if (y.Exception != null)
                        validationError.Errors.Add(y.Exception.Message);
                });
            });

            return new ObjectResult(validationError)
            {
                StatusCode = 400
            };
        }

    }
}
