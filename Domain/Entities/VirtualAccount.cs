using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class VirtualAccount
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        //虛擬帳號的代碼
        public required string VirtualAccountNumber { get; init; } = string.Empty;

        //實體帳號
        public required Guid BankAccountId { get; init; }

        //幣別
        public string Currency { get; init; } = "TWD";

        //收款時間
        public required DateTime StartDate { get; init; }
        public required DateTime EndDate { get; init; }

        //收款額度上限，已收金額
        public required decimal MaxAmount { get; init; }
        public decimal CollectedAmount { get; private set; }

        //帳號狀態
        public VirtualAccountStatus Status { get; private set; } = VirtualAccountStatus.Active;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        //樂觀鎖
        public byte[] RowVersion { get; private set; } = new byte[0];

        //判斷能否接受此筆入金
        public bool CanAcceptPayment(decimal incomingAmount, DateTime currentDateTime)
        {
            if (Status != VirtualAccountStatus.Active)
            {
                return false;
            }
            if (currentDateTime < StartDate || currentDateTime > EndDate)
            {
                return false;
            }
            if (incomingAmount <= 0 || (CollectedAmount + incomingAmount > MaxAmount))
            {
                return false;
            }
            return true;
        }

        //執行入金
        public void ApplyPayment(decimal incomingAmount, DateTime currentDateTime)
        {
            if (!CanAcceptPayment(incomingAmount, currentDateTime))
            {
                throw new InvalidOperationException("入金失敗：金額超限、不在期限內或帳戶狀態異常。");
            }

            CollectedAmount += incomingAmount;

            if (CollectedAmount >= MaxAmount)
            {
                Status = VirtualAccountStatus.Completed;
            }
        }

    }
}
