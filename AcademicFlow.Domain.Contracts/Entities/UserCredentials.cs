using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class UserCredentials: IModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public User User { get; set; }
    }
}
