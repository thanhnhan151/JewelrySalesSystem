﻿using AutoMapper;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RolesController(
            ILogger<RolesController> logger,
            IRoleService roleService, IMapper mapper)
        {
            _logger = logger;
            _roleService = roleService;
            _mapper = mapper;
        }

        /*Change here*/
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleViewModel roleViewModel)
        {
            //var newRole = await _roleService.AddRoleAsync(role);
            //return Ok(newRole);
            //Map the RoleViewModel to a Role object

            // Call the service to add the new role
            var newRole = await _roleService.AddRoleAsync(roleViewModel);

            // Map the new Role back to a RoleViewModel and return it
            //var newRoleViewModel = _mapper.Map<RoleViewModel>(newRole);
            return Ok(newRole);
        }

    }
}
