using Helpers;
using Microsoft.AspNetCore.Mvc;
using Models;
using UserTaskManagementAPI.Controllers;

namespace UserTaskManagementTest
{
    public class TaskControllerTest
    {
        public TaskController taskController;
        public UserTaskCollections userTaskCollections;
        [SetUp]

        public void Setup()
        {
            userTaskCollections = new UserTaskCollections
            {
                taskItems = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Task 1", Status = "Pending", Priority = "Low" },
                new TaskItem { Id = 2, Title = "Task 2", Status = "InProgress", Priority = "Medium" },
                new TaskItem { Id = 3, Title = "Task 3", Status = "Completed", Priority = "High" },
            }
            };

            taskController = new TaskController(userTaskCollections);
        }

        [Test]
        public void GetAllTasks_validate_without_filters_returntrue()
        {
            var result = taskController.GetAllTasks();
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
        }

        [Test]
        public void GetAllTasks_validate_with_Status_filters_returnTrue()
        {
            var result = taskController.GetAllTasks("Pending");
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
        }

        [Test]
        public void GetAllTasks_validate_with_Status_filters_returnFalse()
        {
            var result = taskController.GetAllTasks("ok");
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
        }

        [Test]
        public void GetAllTasks_validate_with_priority_filters_returnTrue()
        {
            var result = taskController.GetAllTasks(string.Empty,"Low");
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
        }

        [Test]
        public void GetAllTasks_validate_with_priority_filters_returnFalse()
        {
            var result = taskController.GetAllTasks(string.Empty,"ok");
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
        }

        [Test]
        public void GetAllTasks_validate_without_data_returnFalse()
        {
            userTaskCollections.taskItems.Clear();
            taskController = new TaskController(userTaskCollections);
            var result = taskController.GetAllTasks();
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
        }

        [Test]
        public void GetAllTasks_validate_withNull_returnFalse()
        {
            userTaskCollections.taskItems = default;
            taskController = new TaskController(userTaskCollections);
            var result = taskController.GetAllTasks();
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
        }
    }
}