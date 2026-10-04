using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ClearingTransaction
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string IdempotencyKey { get; init; }
        public required string ReferenceNumber { get; init; }
        public DateTime TransactionDate { get; init; } = DateTime.UtcNow;

        //內部維護清單，只有自己能改
        private readonly List<JournalEntry> _entries = new();

        //對外公開的唯獨清單
        public IReadOnlyCollection<JournalEntry> Entries => _entries.AsReadOnly();

        //抓取外部傳票
        public void AddEntry(JournalEntry entry)
        {
            _entries.Add(entry);
        }

        public bool IsBalanced()
        {
            if (_entries.Count == 0)
            {
                return false;
            }
            decimal totalCredits = 0;
            decimal totalDebits = 0;
            foreach (var entry in _entries)
            {
                if (entry.Type == Enums.EntryType.Credit)
                {
                    totalCredits += entry.Amount;
                }
                else if (entry.Type == Enums.EntryType.Debit)
                {
                    totalDebits += entry.Amount;
                }
            }
            return totalCredits == totalDebits;
        }
    }
}
