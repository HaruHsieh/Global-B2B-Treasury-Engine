using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BankAccount
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        //實體帳戶號碼
        public required string AccountNumber { get; init; } = string.Empty;

        //餘額
        public decimal Balance { get; private set; } = 0;

        //狀態
        public bool IsActive { get; private set; } = true;

        public byte[] RowVersion { get; private set; } = new byte[0];

        public void Credit(decimal incomingAmount)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("帳戶已停用，無法存款。");
            }
            if (incomingAmount <= 0)
            {
                throw new ArgumentException("存款金額必須大於零。");
            }
            Balance += incomingAmount;
        }
        public void Debit(decimal outgoingAmount)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("帳戶已停用，無法提款。");
            }
            if (outgoingAmount <= 0)
            {
                throw new ArgumentException("提款金額必須大於零。");
            }
            if (Balance < outgoingAmount)
            {
                throw new InvalidOperationException("餘額不足，無法提款。");
            }
            Balance -= outgoingAmount;
        }

    }
}
