﻿using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetBalanceQuery : IRequest<decimal>
    {
        public string Token { get; set; }
    }
}
