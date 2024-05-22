using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sharecipe_IT15_Project.Models.Entities;

namespace Sharecipe_IT15_Project.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SharecipeUser class
public class SharecipeUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FullName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public DateOnly Birthdate { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Address { get; set; }


    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Bio { get; set; } = string.Empty;

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string ProfPIc { get; set; } = string.Empty;

    public string coverPic { get; set; } = string.Empty;

    public virtual ICollection<Post> UserPost { get; set; } // Singular name for clarity

}

