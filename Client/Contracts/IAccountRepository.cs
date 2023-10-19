﻿using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;
using Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<AccountDto, Guid>
    {

        Task<ResponseOKHandler<TokenDto>> Login(LoginDto loginDto);
    }
}
