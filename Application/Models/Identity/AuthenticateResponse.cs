﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Models.Identity;

public class AuthenticateResponse {

    public bool Succeeded { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
    public string? Token { get; set; }
}

