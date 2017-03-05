using Clone;
using System;
using System.Collections.Generic;

namespace CloneTest.TestClasses
{
    public class Person : BasicClone<Person>
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IDNumber { get; set; }
        public List<Illness> Illnesses { get; set; }
    }
}
