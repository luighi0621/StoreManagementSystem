using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreManagement.Dal.Interfaces;
using StoreManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StoreManagement.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        public readonly IUserRepository MockUserRepository;
        public TestContext TextContext { get; set; }

        public UserRepositoryTest()
        {
            IList<User> users = new List<User>()
            {
                new User()
                {
                    Firstname = "Jose",
                    Lastname = "Perez",
                    Login = "jperez",
                    Password = "jperez",
                    Id = 1
                },
                new User()
                {
                    Firstname = "Maria",
                    Lastname = "lopez",
                    Login = "mlopez",
                    Password = "mlopez",
                    Id = 2
                },
                new User()
                {
                    Firstname = "Clelia",
                    Lastname = "Aguilar",
                    Login = "caguilar",
                    Password = "caguilar",
                    Id = 3
                }
            };
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserRepository.Setup(mr => mr.GetAll()).Returns(users);

            _mockUserRepository.Setup(mr => mr.Count()).Returns(users.Count);

            _mockUserRepository.Setup(mr => mr.GetAllAsync()).ReturnsAsync(users);

            _mockUserRepository.Setup(mr => mr.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(
                (Expression<Func<User, bool>> expression) =>
                {
                    var userQuery = users.Where(expression.Compile()).FirstOrDefault();
                    return userQuery;
                }
                );


            _mockUserRepository.Setup(mr => mr.Get(It.IsAny<Expression<Func<User, bool>>>())).Returns(
                (Expression<Func<User, bool>> expression) => 
                {
                    var userQuery = users.Where(expression.Compile()).FirstOrDefault();
                    return userQuery;
                }
                );
            _mockUserRepository.Setup(mr => mr.Delete(It.IsAny<User>()))
                .Callback(
                (User us) => 
                {
                    users.Remove(us);
                }
                ).Verifiable();
            _mockUserRepository.Setup(mr => mr.Create(It.IsAny<User>()))
                .Callback(
                (User us) => {
                    us.Id = users.Count+1;
                    users.Add(us);
                }
                );
            _mockUserRepository.Setup(mr => mr.Update(It.IsAny<User>()))
                .Callback(
                (User us) => {
                    int index = users.IndexOf(us);
                    if (index != -1)
                    {
                        users[index] = us;
                    }
                }
                )
                .Verifiable();
            _mockUserRepository.Setup(mr => mr.SaveAsync()).ReturnsAsync(1);

            this.MockUserRepository = _mockUserRepository.Object;
        }

        [TestMethod]
        public void SaveAsyncRepository()
        {
            MockUserRepository.SaveAsync();
            _mockUserRepository.Verify(x => x.SaveAsync());
        }

        [TestMethod]
        public void ReturnCountOfRepository()
        {
            Assert.AreEqual(3, MockUserRepository.Count());
        }

        [TestMethod]
        public void ReturnAllUsers()
        {
            IList<User> usersTest = this.MockUserRepository.GetAll();

            Assert.IsNotNull(usersTest);
            Assert.AreEqual(3, usersTest.Count);
        }

        [TestMethod]
        public void ReturnAllUsersAsync()
        {
            var usersTest = MockUserRepository.GetAllAsync();
            var resultList = usersTest.Result;
            Assert.IsNotNull(resultList);
            Assert.AreEqual(3, resultList.Count);
        }

        [TestMethod]
        public void ReturnAUserAsyncDependingQuery()
        {
            var usersTest = this.MockUserRepository.GetAsync(u => u.Id == 3);
            User user = usersTest.Result;
            Assert.IsNotNull(usersTest);
            Assert.AreEqual("caguilar", user.Login);
            Assert.AreEqual("Clelia", user.Firstname);
            Assert.AreEqual("Aguilar", user.Lastname);
        }

        [TestMethod]
        public void ReturnAUserDependingQuery()
        {
            User usersTest = this.MockUserRepository.Get(u=> u.Id == 3);
            Assert.IsNotNull(usersTest);
            Assert.AreEqual("caguilar", usersTest.Login);
            Assert.AreEqual("Clelia", usersTest.Firstname);
            Assert.AreEqual("Aguilar", usersTest.Lastname);
        }

        [TestMethod]
        public void DeleteUserFromRepository()
        {
            User toDelete = MockUserRepository.Get(u => u.Id == 3);
            MockUserRepository.Delete(toDelete);
            IList<User> list = MockUserRepository.GetAll();
            User deleted = MockUserRepository.Get(u => u.Id == 3);
            Assert.IsNull(deleted);
            Assert.IsNotNull(list);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void CreateUserRepository()
        {
            User newUser = new User()
                            {
                                Firstname = "Karen",
                                Lastname = "Valenzuela",
                                Login = "kValenz",
                                Password = "kValenz"
            };
            MockUserRepository.Create(newUser);
            IList<User> list = MockUserRepository.GetAll();
            User Created = MockUserRepository.Get(u => u.Lastname == "Valenzuela");
            Assert.IsNotNull(Created);
            Assert.AreNotEqual( default(int) ,Created.Id);
            Assert.IsNotNull(list);
            Assert.AreEqual(4, list.Count);
        }

        [TestMethod]
        public void UpdateUserRepository()
        {
            User usersTest = this.MockUserRepository.Get(u => u.Id == 3);
            usersTest.Firstname = "Modified";
            usersTest.Lastname = "Modified";
            usersTest.Login = "Modified";
            MockUserRepository.Update(usersTest);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()));
            User modified = this.MockUserRepository.Get(u => u.Id == 3);
            IList<User> list = MockUserRepository.GetAll();
            Assert.IsNotNull(modified);
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(usersTest.Firstname, modified.Firstname);
            Assert.AreEqual(usersTest.Login, modified.Login);
            Assert.AreEqual(usersTest.Lastname, modified.Lastname);
        }
    }
}
