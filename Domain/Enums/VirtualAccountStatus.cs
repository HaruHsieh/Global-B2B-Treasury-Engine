using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum VirtualAccountStatus
    {
        Active = 1, //生效
        Completed = 2, //完成，關閉
        Suspended = 3, //暂停
        Closed = 4 //關閉
    }
}
