using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class JobApp
    {
        public int JobAppId { get; set; }

        public string Name { get; set; }

        public List<FormInput> Answers { get; set; }
    }
}
