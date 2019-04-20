﻿using charity_management_system.Models;
using charity_management_system.Utils;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace charity_management_system.Repositories
{
    public class PaidEmployeeRepo : IRepository<PaidEmployee>
    {
        private OracleConnection connection;
        private OracleCommand command;
        public PaidEmployeeRepo()
        {
            connection = DBManager.instance.connection;
            command = new OracleCommand();
            command.Connection = connection;
        }
        public bool delete(PaidEmployee model)
        {
            throw new NotImplementedException();
        }

        public List<PaidEmployee> find(string column, string value)
        {
            throw new NotImplementedException();
        }

        public List<PaidEmployee> findAll()
        {
            throw new NotImplementedException();
        }

        public PaidEmployee findByID(string id)
        {
            command.CommandText = "FIND_PAID_EMPLOYEE_BY_ID";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("emp_ssn", id);
            command.Parameters.Add("ssn", ParameterDirection.Output);
            command.Parameters.Add("name", ParameterDirection.Output);
            command.Parameters.Add("mobile", ParameterDirection.Output);
            command.Parameters.Add("birth_date", ParameterDirection.Output);
            command.Parameters.Add("gender", ParameterDirection.Output);
            command.Parameters.Add("address_line1", ParameterDirection.Output);
            command.Parameters.Add("address_line2", ParameterDirection.Output);
            command.Parameters.Add("city", ParameterDirection.Output);
            command.Parameters.Add("governorate", ParameterDirection.Output);
            command.Parameters.Add("email", ParameterDirection.Output);
            command.Parameters.Add("branch_id", ParameterDirection.Output);
            command.Parameters.Add("salary", ParameterDirection.Output);
            command.Parameters.Add("department_name", ParameterDirection.Output);

            OracleDataReader reader = command.ExecuteReader();
            PaidEmployee paidEmployee;
            if (reader.Read()) {
                paidEmployee = new PaidEmployee
                {
                    SSN = int.Parse(reader["ssn"].ToString()),
                    name = reader["name"].ToString(),
                    mobile = reader["mobile"].ToString(),
                    birthDate = Convert.ToDateTime(reader["birth_date"]),
                    gender = char.Parse(reader["gender"].ToString()),
                    addressLine1 = reader["address_line1"].ToString(),
                    addressLine2 = reader["address_line2"].ToString(),
                    city = reader["city"].ToString(),
                    governorate = reader["governorate"].ToString(),
                    email = reader["email"].ToString(),
                    salary = float.Parse(reader["salary"].ToString()),
                    branch = new Branch { id = int.Parse(reader["branch_id"].ToString()) },
                    department = new Department { name = reader["department_name"].ToString() }
                };
            }
            else {
                reader.Close();
                return null;
            }
            reader.Close();
            return paidEmployee;

        }

        public PaidEmployee save(PaidEmployee model)
        {
            command.CommandText = "SAVE_PAID_EMPLOYEE";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("ssn", model.SSN);
            command.Parameters.Add("name", model.name);
            command.Parameters.Add("mobile", model.mobile);
            command.Parameters.Add("birth_date", model.birthDate);
            command.Parameters.Add("gender", model.gender);
            command.Parameters.Add("address_line1", model.addressLine1);
            command.Parameters.Add("address_line2", model.addressLine2);
            command.Parameters.Add("city", model.city);
            command.Parameters.Add("governorate", model.governorate);
            command.Parameters.Add("email", model.email);
            command.Parameters.Add("branch_id", model.branch.id);
            command.Parameters.Add("salary", model.salary);
            command.Parameters.Add("department_name", model.department);
            int ret = command.ExecuteNonQuery();
            if (ret != -1)
            {
                //return paid_employee;
            }
            return null;
        }

        public bool update(PaidEmployee newModel)
        {
            throw new NotImplementedException();
        }
    }
}