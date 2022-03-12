﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.DAL
{

    //this class cannot be access from outside of the class library project (entities)
    //any reference within the class library project to this class will be honoured
    //this is for security and keeps access to the data limited
    internal class WestWindContext : DbContext
    {
        public WestWindContext()
        {

        }

        public WestWindContext(DbContextOptions<WestWindContext> options) : base(options)
        {

        }

        //We need DBsets (set of data from the database) for each entity
        public DbSet<BuildVersion> BuildVersions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Territory> Territories { get; set; }
    }
}
