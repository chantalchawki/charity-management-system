﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using charity_management_system.Models;

namespace charity_management_system.Models
{
    class PaidEmployee : Employee
    {
        float salary;
        Department department;

        public PaidEmployee(int SSN, String mobile, String Name, String addressLine1, String city, String governorate,  float salary) : base(SSN, mobile, Name, addressLine1, city, governorate)
        {
            this.salary = salary;
        }
    }
}