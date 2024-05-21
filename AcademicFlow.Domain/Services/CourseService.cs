﻿using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class CourseService(ICourseRepository courseRepository, ICourseUserRoleRepository courseUserRoleRepository) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ICourseUserRoleRepository _courseUserRoleRepository = courseUserRoleRepository;

        public int? AddCourse(Course course)
        {
            return _courseRepository.Add(course)?.Id;
        }

        public Course? GetCourseById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepository.Update(course);
        }
    }
}

