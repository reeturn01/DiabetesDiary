using DiabetesDiaryPages.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DiabetesDiaryPages.Data
{
    public static class SeedIdentityData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("RoleManager is null !");
            }

            IdentityResult identityResult;

            if (await roleManager.RoleExistsAsync(Constants.Roles.DoctorRole) == false)
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(Constants.Roles.DoctorRole));

                if (identityResult.Succeeded == false) 
                {
                    var errorMessages = identityResult.Errors
                        .Select((e, i) => string.Format("Error {0}: Code = {1}, Description = {2}", i, e.Code, e.Description))
                        .ToArray();

                    throw new Exception(string.Format("Errors ocurred while adding {0} role %n", Constants.Roles.DoctorRole, string.Join("%n", errorMessages)));
                }
            }

            if (await roleManager.RoleExistsAsync(Constants.Roles.PatientRole) == false) 
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(Constants.Roles.PatientRole));

                if (identityResult.Succeeded == false) 
                {
                    var errorMessages = identityResult.Errors
                        .Select((e, i) => string.Format("Error {0}: Code = {1}, Description = {2}", i, e.Code, e.Description))
                        .ToArray();

                    throw new Exception(string.Format("Errors ocurred while adding {0} role %n", Constants.Roles.PatientRole, string.Join("%n", errorMessages)));
                }
            }

        }
    }
}
