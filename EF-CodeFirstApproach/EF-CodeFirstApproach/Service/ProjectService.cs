using EF_CodeFirstApproach.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CodeFirstApproach.Model;

namespace EF_CodeFirstApproach.Service
{
    internal class ProjectService
    {
        ProjectRepository projectRepository;
        public ProjectService() 
        {
            projectRepository = new ProjectRepository();
        }

        public void ShowAllProjects()
        {
             var projects = projectRepository.GetAllProjects();
            Console.WriteLine($"{"ID",-10}{"Project Name",-40}");
            foreach(var project in projects)
            {
                Console.WriteLine($"{project.ProjectID,-10}{project.ProjectName,-40}");
            }
        }
         public void CreateNewProject()
        {
            string projectName = InputValidation.GetProjectName();
            projectRepository.AddProjects(
                new List<Project>() { 
                    new Project() { ProjectName = projectName } 
                });

        }
    }
}
