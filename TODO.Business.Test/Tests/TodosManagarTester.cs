using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TODO.Business.DTOs;
using TODO.Business.Services.Abstract;
using TODO.Business.Services.Concrete;
using TODO.DataAccess.Models;
using TODO.DataAccess.Repositories.EfCore.Abstract;
using TODO.DataAccess.Repositories.EfCore.Concrete;
using TODO.Shared.Dtos;
using Xunit;
using System.Linq;

namespace TODO.Business.Test.Tests
{
    public sealed class TodosManagarTester
    {
        private readonly Mock<IEfCoreTodosRepository> _mockTodosRepo;
        private readonly ITodosManager _todosManager;
        private readonly List<Todo> _todos;

        public TodosManagarTester()
        {
            _mockTodosRepo = new Mock<IEfCoreTodosRepository>();

            _todosManager = new TodosManager(_mockTodosRepo.Object);

            _todos = new List<Todo>()
                  {
                    new Todo(){TodoId=1, Title = "item1",DueDate = DateTime.Now.AddDays(1).Date,IsMarked=false},
                    new Todo(){TodoId=2, Title = "item2",DueDate = DateTime.Now.AddDays(-1).Date,IsMarked=true},
                    new Todo(){TodoId=3, Title = "item3",DueDate = DateTime.Now.AddMonths(-1).Date,IsMarked=false},
                    new Todo(){TodoId=4, Title = "item4",DueDate = DateTime.Now.AddMonths(-1).Date,IsMarked=true},
                   };
        }

        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsResponseIEnumarableTodoReturnDto()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            var pendingOverdues = Assert.IsAssignableFrom<Response<IEnumerable<TodoReturnDto>>>(result);
        }

        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsCountOne()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            Assert.Single(result.Data);
        }

        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsContainsData()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            Assert.NotEmpty(result.Data);
        }


        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsMarkedIsFalse()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            Assert.All(result.Data,
                  item => Assert.True(item.IsMarked.Equals(false))
                );
        }


        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsDueDateGreaterOrEqualThanNow()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            Assert.Collection(result.Data, (item) =>
            {
                DateTime dueDate = Convert.ToDateTime(item.DueDate);
                Assert.True(dueDate >= DateTime.Now);
            });

        }


        [Fact]
        public async void GetPendingTodosAsync_ActionExecute_ReturnsDueDateFormatted()
        {
            _mockTodosRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_todos);

            var result = await _todosManager.GetPendingTodosAsync();

            Assert.Collection(result.Data, (item) =>
            {
                string dueDate = Convert.ToDateTime(item.DueDate).ToString("yyyy-MM-dd");

                Assert.Equal(dueDate,item.DueDate);
            });

        }

    }
}
