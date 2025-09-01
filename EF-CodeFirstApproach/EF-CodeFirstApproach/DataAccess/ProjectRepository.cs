using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CodeFirstApproach.Model;

namespace EF_CodeFirstApproach.DataAccess
{
    internal class ProjectRepository
    {
        public List<Project> GetAllProjects()
        {
            try
            {

                using (var context = new EmployeeContext())
                {
                    var projects = context.Projects.ToList();
                    return projects;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : while get all the projects from the Database.. check the Data Access layer...");
                Console.WriteLine(ex);
                throw;
            }
        }

        public void AddProjects(List<Project> projects)
        {
            using (var context = new EmployeeContext())
            {
                context.Projects.AddRange(projects);
                context.SaveChanges();
            }
        }
    }
}
