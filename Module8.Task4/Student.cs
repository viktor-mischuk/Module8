﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public Student(string name, string group, DateTime dateOfBirth)
        //{
        //    Name = name;
        //    Group = group;
        //    DateOfBirth = dateOfBirth;
        //}
    }
}
