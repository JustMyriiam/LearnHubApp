using System.ComponentModel.DataAnnotations;

namespace PFA.Models
{
    public class After1950Attribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date.Year >= 1950;
            }
            return false;
        }
    }
}
