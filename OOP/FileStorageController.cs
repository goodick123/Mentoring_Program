using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using Newtonsoft.Json;
using OOP.Helpers;
using OOP.Interfaces;

namespace OOP
{
    public class FileStorageController : IFileStorageController
    {
        private readonly IFileSystemService<BookDTO> _bookService;
        private readonly IFileSystemService<LocalizedBookDTO> _localizedBookService;
        private readonly IFileSystemService<PatentDTO> _patentService;
        private readonly IFileSystemService<MagazineDTO> _magazineService;
        private readonly ConsolePrintHelper  helper = new();

        public FileStorageController(
            IFileSystemService<BookDTO> bookService, 
            IFileSystemService<LocalizedBookDTO> localizedBookService, 
            IFileSystemService<PatentDTO> patentService,
            IFileSystemService<MagazineDTO> magazineService)
        {
            _bookService = bookService;
            _localizedBookService = localizedBookService;
            _patentService = patentService;
            _magazineService = magazineService;
        }

        public void RunProgram()
        {
            InsertTestData();

            while (true)
            {
                var type = helper.PrintStart();

                if (string.IsNullOrEmpty(type))
                {
                    Console.WriteLine("Provide file type.");
                    continue;
                }

                var idChoice = helper.PrintId();

                object result;

                if (char.IsWhiteSpace(idChoice))
                {
                    result = JsonConvert.SerializeObject(GetItems(type), Formatting.Indented);
                }
                else
                {
                    var id = (int)char.GetNumericValue(idChoice);

                    result = JsonConvert.SerializeObject(GetItem(type, id), Formatting.Indented);
                }

                var exit = helper.PrintExit(result);

                if (exit == '1')
                {
                    break;
                }
            }
        }

        public object GetItem(string type, int id)
        {
            object result = null;
            switch (type)
            {
                case ConstantsClass.BOOK:
                    {
                    result = _bookService.GetDocument(type, id);
                    break;
                }
                case ConstantsClass.LOCALIZED_BOOK:
                    {
                    result = _localizedBookService.GetDocument(type, id);
                    break;
                }
                case ConstantsClass.PATENT:
                    {
                    result = _patentService.GetDocument(type, id);
                    break;
                }
                case ConstantsClass.MAGAZINE:
                    {
                    result = _magazineService.GetDocument(type, id);
                    break;
                }
            }

            return result;
        }

        public object GetItems(string type)
        {
            object result = null;
            switch (type)
            {
                case ConstantsClass.BOOK:
                {
                    result = _bookService.GetAllDocuments(type);
                    break;
                }
                case ConstantsClass.LOCALIZED_BOOK:
                    {
                    result = _localizedBookService.GetAllDocuments(type);
                    break;
                }
                case ConstantsClass.PATENT:
                    {
                    result = _patentService.GetAllDocuments(type);
                    break;
                }
                case ConstantsClass.MAGAZINE:
                    {
                    result = _magazineService.GetAllDocuments(type);
                    break;
                }
            }

            return result;
        }

        private void InsertTestData()
        {
            foreach (var bookDto in FakeData.GetFakeBookDtos())
            {
                _bookService.CreateDocument(bookDto);
            }

            foreach (var patentDto in FakeData.GetFakePatentDtos())
            {
                _patentService.CreateDocument(patentDto);
            }
        }
    }
}
