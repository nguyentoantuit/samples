using CloneTest.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloneTest
{
    [TestClass]
    public class CloneTest
    {
        [TestMethod]
        public void ShallowClone()
        {
            var person = CreateTestPerson();
            var clonedPerson = person.ShallowClone();

            Assert.AreEqual(person.IDNumber, clonedPerson.IDNumber);
            person.Illnesses.Add(new Illness { DateOfIllness = DateTime.Today, IllnessDescription = "Test illness 3", Treatment = "Do Exercise equetlcent regularly" });

            Assert.AreEqual(person.Illnesses.Count, clonedPerson.Illnesses.Count);
        }

        [TestMethod]
        public void FullClone()
        {
            var person = CreateTestPerson();
            person.Illnesses.First().NextVisit.Add(DateTime.Today);
            var clonedPerson = person.FullClone();

            Assert.AreEqual(person.IDNumber, clonedPerson.IDNumber);
            person.Illnesses.Add(new Illness { DateOfIllness = DateTime.Today, IllnessDescription = "Test illness 3", Treatment = "Do Exercise equetlcent regularly" });
            person.Illnesses.First().NextVisit.Add(DateTime.Today.AddDays(-7));

            person.Illnesses.First().DateOfIllness = DateTime.Today.AddDays(1);

            Assert.AreNotEqual(person.Illnesses.Count, clonedPerson.Illnesses.Count);
            Assert.AreNotEqual(person.Illnesses.First().NextVisit.Count, clonedPerson.Illnesses.First().NextVisit.Count);
            Assert.AreEqual(1, clonedPerson.Illnesses.First().NextVisit.Count);
            Assert.AreEqual(2, person.Illnesses.First().NextVisit.Count);
            Assert.AreNotEqual(person.Illnesses.First().DateOfIllness, clonedPerson.Illnesses.First().DateOfIllness);
        }

        public Person CreateTestPerson()
        {
            var person = new Person
            {
                DateOfBirth = new DateTime(1991, 12, 26),
                IDNumber = "123",
                Name = "Nguyen Toan"
            };

            var illnesses = new List<Illness>();
            illnesses.Add(new Illness { DateOfIllness = DateTime.Today, IllnessDescription = "Test illness 1", Treatment = "Do Exercise equetlcent regularly" });
            illnesses.Add(new Illness { DateOfIllness = DateTime.Today.AddDays(-1), IllnessDescription = "Test illness 2", Treatment = "Do Exercise equetlcent regularly. Don't use cafein drink" });
            person.Illnesses = illnesses;
            return person;
        }
    }
}
