using Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Library.DAL.Entities
{
    public class CsvDataHandler
    {
        private readonly string _folderPath;
        private readonly string _bookspath;
        private readonly string _memberspath;
        private readonly string _recordsPath;
        private readonly string _metaPath;

        public CsvDataHandler()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            _folderPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\Library.DAL\Data"));

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            _bookspath = Path.Combine(_folderPath, "books.csv");
            _memberspath = Path.Combine(_folderPath, "members.csv");
            _recordsPath = Path.Combine(_folderPath, "records.csv");
            _metaPath = Path.Combine(_folderPath, "metadata.csv");
        }

        public void SaveData(List<Book> books, List<Member> members, List<BorrowRecord> records, int nextBookId, int nextMemberId, int nextRecordId)
        {
            var bookLines = new List<String> { "BookId,Title,Author,IsAvailable" };
            foreach (var b in books)
            {
                bookLines.Add($"{b.BookId},{EscapeCsv(b.Title)},{EscapeCsv(b.Author)},{b.IsAvailable}");
            }
            File.WriteAllLines(_bookspath, bookLines);


            var memberLines = new List<string> { "MemberId,Name,Email" };
            foreach (var m in members)
            {
                memberLines.Add($"{m.MemberId},{EscapeCsv(m.Name)},{EscapeCsv(m.Email)}");
            }
            File.WriteAllLines(_memberspath, memberLines);

            var recordLines = new List<string> { "RecordId,BookId,memberId,BorrowDate,ReturnDate,IsReturned" };
            foreach (var r in records)
            {
                recordLines.Add($"{r.RecordId},{r.BookId},{r.MemberId},{r.BorrowDate},{r.ReturnDate},{r.IsReturned}");
            }
            File.WriteAllLines(_recordsPath, recordLines);
            var metaLines = new List<string> { "NextBookId,NextMemberId,NextRecordId", $"{nextBookId},{nextMemberId},{nextRecordId}" };
            File.WriteAllLines(_metaPath, metaLines);
        }

        public (List<Book> books, List<Member> members, List<BorrowRecord> records, int nextBookId, int nextMemberId, int nextRecordId) LoadData()
        {
            var books = new List<Book>();
            var members = new List<Member>();
            var records = new List<BorrowRecord>();
            int nextBookId = 1, nextMemberId = 1, nextRecordId = 1;


           if (File.Exists(_bookspath))
            {
                var lines = File.ReadAllLines(_bookspath);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 4)
                    {
                        books.Add(new Book(
                            
                           int.Parse(parts[0]),
                            UnescapeCsv(parts[1]),
                            UnescapeCsv(parts[2]),
                           bool.Parse(parts[3]),
                            DateTime.Now
                        ));
                        
                    }
                }
            }

            if (File.Exists(_memberspath))
            {
                var lines = File.ReadAllLines(_memberspath);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 3)
                    {
                        members.Add(new Member(

                            int.Parse(parts[0]),
                            UnescapeCsv(parts[1]),
                            UnescapeCsv(parts[2]),
                            DateTime.Now
                        ));
                    }
                }
            }

            if (File.Exists(_recordsPath))
            {
                var lines = File.ReadAllLines(_recordsPath);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 6)
                    {
                        records.Add(new BorrowRecord(

                            int.Parse(parts[0]),
                            int.Parse(parts[1]),
                            int.Parse(parts[2]),
                            DateTime.Parse(parts[3]),
                            string.IsNullOrEmpty(parts[4]) ? (DateTime?)null : DateTime.Parse(parts[4]),
                            bool.Parse(parts[5])
                            
                            ));
                    }
                }
            }
            if (File.Exists(_metaPath))
            {
                var lines = File.ReadAllLines(_metaPath);
                if (lines.Length > 1)
                {
                    var parts = lines[1].Split(',');
                    if (parts.Length >= 3)
                    {
                        nextBookId = int.Parse(parts[0]);
                        nextMemberId = int.Parse(parts[1]);
                        nextRecordId = int.Parse(parts[2]);
                    }
                }
            }
            

            return (books, members, records, nextBookId, nextMemberId, nextRecordId);


        }
        

        private string EscapeCsv(string text) => text?.Contains(",") == true ? $"\"{text}\"" : text;
        private string UnescapeCsv(string text) => text?.Trim('"');

    }
}
