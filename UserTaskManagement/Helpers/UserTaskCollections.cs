using Models;

namespace Helpers
{
    public class UserTaskCollections
    {
        public List<TaskItem> taskItems { get; set; } = new();

        #region Update Status
        public string UpdateStatus(string status)
        {
            string updatedStatus = status;
            if (status == StatusTypes.Pending.ToString())
            {
                updatedStatus = StatusTypes.InProgress.ToString();
            }
            else if (status == StatusTypes.InProgress.ToString())
            {
                updatedStatus = StatusTypes.Completed.ToString();
            }
            else if (status == StatusTypes.Completed.ToString())
            {
                updatedStatus = status;
            }

            return updatedStatus;
        }
        #endregion

        #region Format Status And Date
        public TaskItem FormatStatusAndDate(TaskItem taskItems)
        {
            taskItems.Status = StatusTypes.Pending.ToString();
            taskItems.CreatedAt = DateTime.UtcNow;
            return taskItems;
        }
        #endregion

        #region Validate Title
        public string ValidateTitle(string title)
        {
            string errorMessaeg = string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                errorMessaeg = "Title is required";
            }
            else if (title.Length > 50)
            {
                errorMessaeg = "Title should not exceed 50 characters";
            }

            return errorMessaeg;
        }
        #endregion
    }
}
