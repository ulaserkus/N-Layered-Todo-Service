using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Shared.Dtos
{
    public sealed class Error
    {
        public List<string> Errors { get; set; }

        public Error()
        {
            Errors = new List<string>();
        }
    }
}
