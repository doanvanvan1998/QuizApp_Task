using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Until
{
    public class DateLessThanTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime < DateTime.Now;
            }
            return false;
        }
    }
}
