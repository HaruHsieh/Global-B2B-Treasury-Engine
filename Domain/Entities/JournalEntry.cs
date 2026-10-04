using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class JournalEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required Guid BankAccountId { get; init; }
        public required EntryType Type { get; init; } //借貸方向，存款為 Credit，提款為 Debit
        public required decimal Amount { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
