using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Identity;

public class AddToRoleRequest {

    public string Username { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}
