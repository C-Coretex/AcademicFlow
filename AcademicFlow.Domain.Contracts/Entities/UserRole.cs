using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AcademicFlow.Domain.Contracts.Entities
{
    public class UserRole : IModel
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public RolesEnum Role { get; set; }
        public int Active { get; set; }

        public User User { get; set; }

        public UserRole()
        { }

        public UserRole(int userId, RolesEnum role)
        {
            UserId = userId;
            Role = role;
            Active = 1;
        }

    }
}
