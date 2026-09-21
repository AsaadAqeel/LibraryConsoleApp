using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Library.Models;


// This file to save the data in json
namespace Library.DAL
{
    public class LibraryDataModel
    {
        public List<Book> books { get; set; } = new();
        public List<Member> Members { get; set; } = new();
        public List<BorrowRecord> BorrowRecord { get; set; } = new();
        public int NextBookId { get; set; } = 1;
        public int NextMemberId { get; set; } = 1;
        public int NextRecordId { get; set; } = 1;


    }

    public class LibraryRepository
    {
        private readonly JsonDataHandler _dataHandler;
        public List<Book> Books { get; set; }
        public List<Member> Members { get; set; }
        public List<BorrowRecord> BorrowRecords { get; set; }
        public int NextBookId { get; set; }
        public int NextMemberId { get; set; }
        public int NextRecordId { get; set; }

        public LibraryRepository()
        {
            _dataHandler = new JsonDataHandler();

            var data = _dataHandler.LoadData<LibraryDataModel>();

            Books = data.books ?? new();
            Members = data.Members ?? new();
            BorrowRecords = data.BorrowRecord ?? new();
            NextBookId = data.NextBookId;
            NextMemberId = data.NextMemberId;
            NextRecordId = data.NextRecordId;

        }

        public void SaveChanges()
        {
            var data = new LibraryDataModel
            {
                books = Books,
                Members = Members,
                BorrowRecord = BorrowRecords,
                NextBookId = NextBookId,
                NextMemberId = NextMemberId,
                NextRecordId = NextRecordId
            };
            _dataHandler.SaveData(data);
        }

    }
}
