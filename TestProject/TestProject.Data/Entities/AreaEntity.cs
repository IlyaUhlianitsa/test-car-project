﻿using System;

namespace TestProject.Data.Entities
{
    public class AreaEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}