using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzApp.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Range(1, 100)]
        [Display(Name ="Give number:")]
      
        public int Value { get; set; }
        public string IP { get; set; }
        private string _serverAnswer;
        [Display(Name="Answer")]
        public string ServerAnswer
        {
            get => _serverAnswer;
            set
            {
                SetServerAnswer();
            }
        }
        public void SetServerAnswer()
        {
            bool anyConditionWereTrue = false;
            if (Value < 1 || Value > 100)
            {
                _serverAnswer = "Wrong number";
                return;
            }
            if (Value % 3 == 0)
            {
                _serverAnswer += "fizz";
                anyConditionWereTrue = true;
            }
            if (Value % 5 == 0)
            {
                _serverAnswer += "buzz";
                anyConditionWereTrue = true;
            }
            if (Value % 7 == 0)
            {
                _serverAnswer += "wizz";
                anyConditionWereTrue = true;
            }
            if (anyConditionWereTrue)
                return;
            else
                _serverAnswer = Value.ToString();
        }
    }
}
