﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using openlab_project.Data;
using openlab_project.Models;
using openlab_project;

namespace OpenLabProject1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuildController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuildController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<GuildDetails> GetGuildInformation()
        {
            IEnumerable<GuildInfo> dbGuilds = _context.Guild;

            return dbGuilds.Select(dbGuilds => new GuildDetails
            {
                Id = dbGuilds.Id,
                Name = dbGuilds.Name,
                Description = dbGuilds.Description,
                Capacity = dbGuilds.Capacity,
                MembersCount = GetguildMembersCount(dbGuilds.Id)
            });
        }

        private int GetguildMembersCount(int guildId)
        {
            IQueryable<ApplicationUser> users = _context.Users.Include(applicationUser => applicationUser.GuildInfo).AsNoTracking();

            return users.Where(u => u.GuildInfo.Id == guildId).Count();
        }

    }
}
