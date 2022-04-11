using System;
using Domain.Entities;

namespace Application.BindingModels
{
    public class JobAppSubmittedResponse
    {
        public bool isQualified { get; set; }

        // TODO - Change JobApp to a reduced property set response object
        public JobApp JobApp { get; set; }
    }
}
