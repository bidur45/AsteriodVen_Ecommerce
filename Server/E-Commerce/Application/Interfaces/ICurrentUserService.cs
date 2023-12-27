﻿using Domain.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Name { get; }
        User UserInfo { get; }
    }
}
