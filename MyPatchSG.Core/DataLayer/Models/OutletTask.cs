using System;
using System.Collections.Generic;
using System.Text;

namespace MyPatchSG.DL.Models
{
    public class OutletTask
    {
        public string CUSTOMER_ID { get; set; }
        public double SEQ_NO { get; set; }
        public string PROJECT_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string TASK_DESC { get; set; }

        public OutletTask()
        {

        }

        public string getCustomerID()
        {
            if (this.CUSTOMER_ID == null)
            {
                return "";
            }

            return this.CUSTOMER_ID;
        }
        public void setCustomerID(string CustomerID) { this.CUSTOMER_ID = CustomerID; }

        public string getSeqNO()
        {
            return this.SEQ_NO.ToString();
        }

        public void setSeqNO(double SeqNo) { this.SEQ_NO = SeqNo; }
        public string getProjectCode()
        {
            if (this.PROJECT_CODE == null)
            {
                return "";
            }

            return this.PROJECT_CODE;
        }
        public void setProjectCode(string ProjectCode) { this.PROJECT_CODE = ProjectCode; }
        public string getProjectName()
        {
            if (this.PROJECT_NAME == null)
            {
                return "";
            }

            return this.PROJECT_NAME;
        }
        public void setProjectName(string ProjectName) { this.PROJECT_NAME = ProjectName; }
        public string getTaskDesc()
        {
            if (this.TASK_DESC == null)
            {
                return "";
            }

            return this.TASK_DESC;
        }
        public void setTaskDesc(string TaskDesc) { this.TASK_DESC = TaskDesc; }

    }
}
