using System;
namespace Domain.Entities
{
    public class FormInput
    {
        public int FormInputId { get; set; }

        // TODO - Add Newtonsoft decorator to serialize as "Id"
        public int JobCriteriaId { get; set; }
        public virtual JobCriteria Criteria { get; set; }

        public string Answer { get; set; }
    }
}
